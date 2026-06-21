using System.Net;
using System.Text.Json;
using CashFlow.Communication.Response;
using CashFlow.Communication.Response.Expense;
using FluentAssertions;
using WebApi.Tests.Utils;

namespace WebApi.Tests.Expense.GetAll;

public class GetAllExpenseTest(CustomWebApplicationFactory factory) : CashFlowClassFixture(webApplicationFactory: factory)
{
    private const string Method = "api/Expenses";
    
    // [Fact]
    // public async Task Success()
    // {
    //     HttpResponseMessage result = await DoGet(requestUri: Method, token: factory.Token);
    //
    //     result.StatusCode.Should().Be(expected: HttpStatusCode.OK);
    //
    //     Stream body = await result.Content.ReadAsStreamAsync();
    //
    //     JsonDocument response = await JsonDocument.ParseAsync(utf8Json: body);
    //
    //     response.RootElement.GetProperty(propertyName: nameof(ResponseGetAllExpensesJson.ListAllExpenses).HandlePropertyName()).EnumerateArray().Should().NotBeNullOrEmpty();
    // }
}