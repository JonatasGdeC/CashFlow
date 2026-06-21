using System.Globalization;
using System.Net;
using System.Text.Json;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Requests.Expense;
using CashFlow.Communication.Response;
using CashFlow.Exception;
using CommomTestsUtilies.Requests;
using FluentAssertions;
using WebApi.Tests.InlineData;
using WebApi.Tests.Utils;

namespace WebApi.Tests.Expense.Register;

public class RegisterExpenseTest(CustomWebApplicationFactory factory) : CashFlowClassFixture(webApplicationFactory: factory)
{
    private const string Method = "api/Expenses";
    
    [Fact]
    public async Task Success()
    {
        RequestRegisterExpenseJson request = RequestRegisterExpenseJsonBuilder.Build();

        HttpResponseMessage result = await DoPost(requestUri: Method, request: request, token: factory.Token);

        result.StatusCode.Should().Be(expected: HttpStatusCode.Created);
        Stream body = await result.Content.ReadAsStreamAsync();
        JsonDocument response = await JsonDocument.ParseAsync(utf8Json: body);
        
        response.RootElement.GetProperty(propertyName: nameof(RequestRegisterExpenseJson.Title).HandlePropertyName()).GetString().Should().NotBeNullOrEmpty().And.Be(expected: request.Title);
    }
    
    [Fact]
    public async Task Error_Not_Authorization()
    {
        RequestRegisterExpenseJson request = RequestRegisterExpenseJsonBuilder.Build();
        
        HttpResponseMessage result = await DoPost(requestUri: Method, request: request);
    
        result.StatusCode.Should().Be(expected: HttpStatusCode.Unauthorized);
    }
    
    [Theory]
    [ClassData(@class: typeof(CultureInlineDataTest))]
    public async Task Error_Empty_Title(string culture)
    {
        RequestRegisterExpenseJson request = RequestRegisterExpenseJsonBuilder.Build();
        request.Title = string.Empty;
    
        HttpResponseMessage result = await DoPost(requestUri: Method, request: request, token: factory.Token, culture: culture);
    
        result.StatusCode.Should().Be(expected: HttpStatusCode.BadRequest);
        
        Stream body = await result.Content.ReadAsStreamAsync();
        JsonDocument response = await JsonDocument.ParseAsync(utf8Json: body);
        JsonElement.ArrayEnumerator errors = response.RootElement.GetProperty(propertyName: nameof(ResponseErrorJson.ErrorMessages).HandlePropertyName()).EnumerateArray();
        string? expectedMessage = ResourceErrorMessage.ResourceManager.GetString(name: nameof(ResourceErrorMessage.EXPENSE_TITLE_REQUIRED), culture: new CultureInfo(name: culture));
        errors.Should().HaveCount(expected: 1).And.Contain(predicate: error => error.GetString()!.Equals(expectedMessage));
    }
}