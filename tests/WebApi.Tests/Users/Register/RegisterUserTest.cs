using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Response;
using CommomTestsUtilies.Requests;
using FluentAssertions;

namespace WebApi.Tests.Users.Register;

public class RegisterUserTest(CustomWebApplicationFactory factory) : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _httpClient = factory.CreateClient();

    [Fact]
    public async Task Success()
    {
        RequestRegisterUserJson request = RequestRegisterUserJsonBuilder.Build();
        
        HttpResponseMessage result = await _httpClient.PostAsJsonAsync(requestUri: "api/User", value: request);

        result.StatusCode.Should().Be(expected: HttpStatusCode.Created);
        Stream body = await result.Content.ReadAsStreamAsync();
        JsonDocument response = await JsonDocument.ParseAsync(utf8Json: body);
        response.RootElement.GetProperty(propertyName: nameof(ResponseRegisterUserJson.Name).ToLower()).GetString().Should().Be(expected: request.Name);
        response.RootElement.GetProperty(propertyName: nameof(ResponseRegisterUserJson.Token).ToLower()).GetString().Should().NotBeNullOrEmpty();
    }
}