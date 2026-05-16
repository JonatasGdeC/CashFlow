using System.Net;
using System.Net.Http.Json;
using CashFlow.Communication.Requests;
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
    }
}