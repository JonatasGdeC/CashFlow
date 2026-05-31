namespace CashFlow.Communication.Response.User;

public record ResponseUserProfileJson
{
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
}