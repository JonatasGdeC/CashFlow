using System.Globalization;
using System.Net;
using System.Text.Json;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Requests.User;
using CashFlow.Communication.Response;
using CashFlow.Communication.Response.User;
using CashFlow.Exception;
using CommomTestsUtilies.Requests;
using FluentAssertions;
using WebApi.Tests.InlineData;
using WebApi.Tests.Utils;

namespace WebApi.Tests.Users.Register;

public class RegisterUserTest(CustomWebApplicationFactory factory) : CashFlowClassFixture(webApplicationFactory: factory)
{
    private const string Method = "api/User";

    [Fact]
    public async Task Success()
    {
        RequestRegisterUserJson request = RequestRegisterUserJsonBuilder.Build();

        HttpResponseMessage result = await DoPost(requestUri: Method, request: request);

        result.StatusCode.Should().Be(expected: HttpStatusCode.Created);
        Stream body = await result.Content.ReadAsStreamAsync();
        JsonDocument response = await JsonDocument.ParseAsync(utf8Json: body);
        
        response.RootElement.GetProperty(propertyName: nameof(ResponseRegisterUserJson.Name).HandlePropertyName()).GetString().Should().Be(expected: request.Name);
        response.RootElement.GetProperty(propertyName: nameof(ResponseRegisterUserJson.Token).HandlePropertyName()).GetString().Should().NotBeNullOrEmpty();
    }

    [Theory]
    [ClassData(@class: typeof(CultureInlineDataTest))]
    public async Task Error_Empty_Name(string culture)
    {
        RequestRegisterUserJson request = RequestRegisterUserJsonBuilder.Build();
        request.Name = string.Empty;

        HttpResponseMessage result = await DoPost(requestUri: Method, request: request, culture: culture);

        result.StatusCode.Should().Be(expected: HttpStatusCode.BadRequest);
        
        Stream body = await result.Content.ReadAsStreamAsync();
        JsonDocument response = await JsonDocument.ParseAsync(utf8Json: body);
        JsonElement.ArrayEnumerator errors = response.RootElement.GetProperty(propertyName: nameof(ResponseErrorJson.ErrorMessages).HandlePropertyName()).EnumerateArray();
        string? expectedMessage = ResourceErrorMessage.ResourceManager.GetString(name: nameof(ResourceErrorMessage.USER_NAME_REQUIRED), culture: new CultureInfo(name: culture));
        errors.Should().HaveCount(expected: 1).And.Contain(predicate: error => error.GetString()!.Equals(expectedMessage));
    }
}