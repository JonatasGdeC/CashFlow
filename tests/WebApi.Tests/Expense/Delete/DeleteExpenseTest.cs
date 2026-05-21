using System.Globalization;
using System.Net;
using System.Text.Json;
using CashFlow.Communication.Response;
using CashFlow.Exception;
using FluentAssertions;
using WebApi.Tests.InlineData;
using WebApi.Tests.Utils;

namespace WebApi.Tests.Expense.Delete;

public class DeleteExpenseTest(CustomWebApplicationFactory factory) : CashFlowClassFixture(webApplicationFactory: factory)
{
    private const string Method = "api/Expenses";

    [Fact]
    public async Task Success()
    {
        HttpResponseMessage result = await DoDelete(requestUri: $"{Method}/{factory.Expense.Id}", token: factory.Token);

        result.StatusCode.Should().Be(expected: HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Error_Not_Authorization()
    {
        HttpResponseMessage result = await DoDelete(requestUri: $"{Method}/{factory.Expense.Id}", token: string.Empty);

        result.StatusCode.Should().Be(expected: HttpStatusCode.Unauthorized);
    }

    [Theory]
    [ClassData(@class: typeof(CultureInlineDataTest))]
    public async Task Error_Expense_Not_Found(string culture)
    {
        HttpResponseMessage result = await DoDelete(requestUri: $"{Method}/{Guid.NewGuid()}", token: factory.Token, culture: culture);

        result.StatusCode.Should().Be(expected: HttpStatusCode.NotFound);

        Stream body = await result.Content.ReadAsStreamAsync();
        JsonDocument response = await JsonDocument.ParseAsync(utf8Json: body);
        JsonElement.ArrayEnumerator errors = response.RootElement.GetProperty(propertyName: nameof(ResponseErrorJson.ErrorMessages).HandlePropertyName()).EnumerateArray();
        string? expectedMessage = ResourceErrorMessage.ResourceManager.GetString(name: nameof(ResourceErrorMessage.EXPENSE_NOT_FOUND), culture: new CultureInfo(name: culture));

        errors.Should().HaveCount(expected: 1).And.Contain(predicate: error => error.GetString()!.Equals(expectedMessage));
    }
}
