namespace CashFlow.Communication.Response.User;

public record ResponseRegisterUserJson
{
    public required string Name { get; init; }
    public required string Token { get; init; }
}