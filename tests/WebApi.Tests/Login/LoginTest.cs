using System.Globalization;
using System.Net;
using System.Text.Json;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Response;
using CashFlow.Domain.Enitites;
using CashFlow.Exception;
using CommomTestsUtilies.Entities;
using FluentAssertions;
using WebApi.Tests.InlineData;
using WebApi.Tests.Utils;

namespace WebApi.Tests.Login;

public class LoginTest(CustomWebApplicationFactory factory) : CashFlowClassFixture(webApplicationFactory: factory)
{
    private const string Method = "api/Login";

    [Fact]
    public async Task Success()
    {
        RequestLoginJson request = new()
        {
            Email = factory.User.Email,
            Password = factory.UserPassword
        };

        HttpResponseMessage response = await DoPost(requestUri: Method, request: request);

        response.StatusCode.Should().Be(expected: HttpStatusCode.OK);

        Stream responseBody = await response.Content.ReadAsStreamAsync();
        JsonDocument responseData = await JsonDocument.ParseAsync(utf8Json: responseBody);

        responseData.RootElement.GetProperty(propertyName: nameof(ResponseRegisterUserJson.Name).HandlePropertyName()).GetString().Should().Be(expected: factory.User.Name);
        responseData.RootElement.GetProperty(propertyName: nameof(ResponseRegisterUserJson.Token).HandlePropertyName()).GetString().Should().NotBeNullOrEmpty();
    }

    [Theory]
    [ClassData(@class: typeof(CultureInlineDataTest))]
    public async Task Error_Login_Invalid(string culture)
    {
        User user = UserBuilder.Build();
        
        RequestLoginJson request = new()
        {
            Email = user.Email,
            Password = user.Password
        };
        
        HttpResponseMessage response = await DoPost(requestUri: Method, request: request, culture: culture);
        
        response.StatusCode.Should().Be(expected: HttpStatusCode.Unauthorized);
    
        Stream responseBody = await response.Content.ReadAsStreamAsync();
        JsonDocument responseData = await JsonDocument.ParseAsync(utf8Json: responseBody);
        JsonElement.ArrayEnumerator errors = responseData.RootElement.GetProperty(propertyName: nameof(ResponseErrorJson.ErrorMessages).HandlePropertyName()).EnumerateArray();
        string? expectedMessage = ResourceErrorMessage.ResourceManager.GetString(name: nameof(ResourceErrorMessage.EMAIL_OR_PASSWORD_INVALID), culture: new CultureInfo(name: culture));
        
        errors.Should().HaveCount(expected: 1).And.Contain(predicate: c => c.GetString()!.Equals(expectedMessage));
    }
}