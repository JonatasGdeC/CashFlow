using System.Globalization;
using System.Net;
using System.Text.Json;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Response;
using CashFlow.Exception;
using FluentAssertions;
using WebApi.Tests.InlineData;
using WebApi.Tests.Utils;

namespace WebApi.Tests.Expense.GetById;

public class GetExpenseByIdTest(CustomWebApplicationFactory factory) : CashFlowClassFixture(webApplicationFactory: factory)
{
    private const string Method = "api/Expenses";
    
    [Fact]
    public async Task Success()
    {
        HttpResponseMessage result = await DoGet(requestUri: $"{Method}/{factory.Expense.Id}", token: factory.Token);

        result.StatusCode.Should().Be(expected: HttpStatusCode.OK);

        Stream body = await result.Content.ReadAsStreamAsync();

        JsonDocument response = await JsonDocument.ParseAsync(utf8Json: body);

        response.RootElement.GetProperty(propertyName: nameof(ResponseExpenseJson.Id).HandlePropertyName()).GetGuid().Should().Be(expected: factory.Expense.Id);
        response.RootElement.GetProperty(propertyName: nameof(ResponseExpenseJson.Title).HandlePropertyName()).GetString().Should().NotBeNullOrWhiteSpace();
        response.RootElement.GetProperty(propertyName: nameof(ResponseExpenseJson.Description).HandlePropertyName()).GetString().Should().NotBeNullOrWhiteSpace();
        response.RootElement.GetProperty(propertyName: nameof(ResponseExpenseJson.Date).HandlePropertyName()).GetDateTime().Should().NotBeAfter(unexpected: DateTime.Today);
        response.RootElement.GetProperty(propertyName: nameof(ResponseExpenseJson.Amount).HandlePropertyName()).GetDecimal().Should().BeGreaterThan(expected: 0);

        int paymentType = response.RootElement.GetProperty(propertyName: nameof(ResponseExpenseJson.PaymentType).HandlePropertyName()).GetInt32();
        Enum.IsDefined(enumType: typeof(PaymentType), value: paymentType).Should().BeTrue();
    }
    
    [Theory]
    [ClassData(@class: typeof(CultureInlineDataTest))]
    public async Task Error_Expense_Not_Found(string culture)
    {
        HttpResponseMessage result = await DoGet(requestUri: $"{Method}/{Guid.NewGuid()}", token: factory.Token, culture: culture);

        result.StatusCode.Should().Be(expected: HttpStatusCode.NotFound);
        
        Stream body = await result.Content.ReadAsStreamAsync();
        JsonDocument response = await JsonDocument.ParseAsync(utf8Json: body);
        JsonElement.ArrayEnumerator errors = response.RootElement.GetProperty(propertyName: nameof(ResponseErrorJson.ErrorMessages).HandlePropertyName()).EnumerateArray();
        string? expectedMessage = ResourceErrorMessage.ResourceManager.GetString(name: nameof(ResourceErrorMessage.EXPENSE_NOT_FOUND), culture: new CultureInfo(name: culture));
        
        errors.Should().HaveCount(expected: 1).And.Contain(predicate: error => error.GetString()!.Equals(expectedMessage));
    }
}