using System.Globalization;
using System.Net;
using System.Text.Json;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Requests.Expense;
using CashFlow.Communication.Response;
using CashFlow.Communication.Response.Expense;
using CashFlow.Exception;
using CommomTestsUtilies.Requests;
using FluentAssertions;
using WebApi.Tests.InlineData;
using WebApi.Tests.Utils;

namespace WebApi.Tests.Expense.Update;

public class UpdateExpenseTest(CustomWebApplicationFactory factory) : CashFlowClassFixture(webApplicationFactory: factory)
{
    private const string Method = "api/Expenses";

    [Fact]
    public async Task Success()
    {
        RequestRegisterExpenseJson request = RequestRegisterExpenseJsonBuilder.Build();

        HttpResponseMessage result = await DoPut(requestUri: $"{Method}/{factory.Expense.Id}", request: request, token: factory.Token);

        result.StatusCode.Should().Be(expected: HttpStatusCode.NoContent);

        HttpResponseMessage getResult = await DoGet(requestUri: $"{Method}/{factory.Expense.Id}", token: factory.Token);
        Stream body = await getResult.Content.ReadAsStreamAsync();
        JsonDocument response = await JsonDocument.ParseAsync(utf8Json: body);

        response.RootElement.GetProperty(propertyName: nameof(ResponseExpenseJson.Id).HandlePropertyName()).GetGuid().Should().Be(expected: factory.Expense.Id);
        response.RootElement.GetProperty(propertyName: nameof(ResponseExpenseJson.Title).HandlePropertyName()).GetString().Should().Be(expected: request.Title);
        response.RootElement.GetProperty(propertyName: nameof(ResponseExpenseJson.Description).HandlePropertyName()).GetString().Should().Be(expected: request.Description);
        response.RootElement.GetProperty(propertyName: nameof(ResponseExpenseJson.Date).HandlePropertyName()).GetDateTime().Should().Be(expected: request.Date);
        response.RootElement.GetProperty(propertyName: nameof(ResponseExpenseJson.Amount).HandlePropertyName()).GetDecimal().Should().Be(expected: request.Amount);

        int paymentType = response.RootElement.GetProperty(propertyName: nameof(ResponseExpenseJson.PaymentType).HandlePropertyName()).GetInt32();
        paymentType.Should().Be(expected: (int)request.PaymentType);
        Enum.IsDefined(enumType: typeof(PaymentType), value: paymentType).Should().BeTrue();
    }

    [Fact]
    public async Task Error_Not_Authorization()
    {
        RequestRegisterExpenseJson request = RequestRegisterExpenseJsonBuilder.Build();

        HttpResponseMessage result = await DoPut(requestUri: $"{Method}/{factory.Expense.Id}", request: request, token: string.Empty);

        result.StatusCode.Should().Be(expected: HttpStatusCode.Unauthorized);
    }

    [Theory]
    [ClassData(@class: typeof(CultureInlineDataTest))]
    public async Task Error_Expense_Not_Found(string culture)
    {
        RequestRegisterExpenseJson request = RequestRegisterExpenseJsonBuilder.Build();

        HttpResponseMessage result = await DoPut(requestUri: $"{Method}/{Guid.NewGuid()}", request: request, token: factory.Token, culture: culture);

        result.StatusCode.Should().Be(expected: HttpStatusCode.NotFound);

        Stream body = await result.Content.ReadAsStreamAsync();
        JsonDocument response = await JsonDocument.ParseAsync(utf8Json: body);
        JsonElement.ArrayEnumerator errors = response.RootElement.GetProperty(propertyName: nameof(ResponseErrorJson.ErrorMessages).HandlePropertyName()).EnumerateArray();
        string? expectedMessage = ResourceErrorMessage.ResourceManager.GetString(name: nameof(ResourceErrorMessage.EXPENSE_NOT_FOUND), culture: new CultureInfo(name: culture));

        errors.Should().HaveCount(expected: 1).And.Contain(predicate: error => error.GetString()!.Equals(expectedMessage));
    }

    [Theory]
    [ClassData(@class: typeof(CultureInlineDataTest))]
    public async Task Error_Empty_Title(string culture)
    {
        RequestRegisterExpenseJson request = RequestRegisterExpenseJsonBuilder.Build();
        request.Title = string.Empty;

        HttpResponseMessage result = await DoPut(requestUri: $"{Method}/{factory.Expense.Id}", request: request, token: factory.Token, culture: culture);

        result.StatusCode.Should().Be(expected: HttpStatusCode.BadRequest);

        Stream body = await result.Content.ReadAsStreamAsync();
        JsonDocument response = await JsonDocument.ParseAsync(utf8Json: body);
        JsonElement.ArrayEnumerator errors = response.RootElement.GetProperty(propertyName: nameof(ResponseErrorJson.ErrorMessages).HandlePropertyName()).EnumerateArray();
        string? expectedMessage = ResourceErrorMessage.ResourceManager.GetString(name: nameof(ResourceErrorMessage.EXPENSE_TITLE_REQUIRED), culture: new CultureInfo(name: culture));

        errors.Should().HaveCount(expected: 1).And.Contain(predicate: error => error.GetString()!.Equals(expectedMessage));
    }
}
