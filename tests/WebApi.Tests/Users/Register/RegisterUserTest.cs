using System.Net;
using System.Net.Http.Json;
using CashFlow.Communication.Requests;
using CommomTestsUtilies.Requests;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace WebApi.Tests.Users.Register;

public class RegisterUserTest(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
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