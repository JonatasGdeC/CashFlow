namespace CashFlow.Communication.Requests.User;

public record RequestUpdateUserJson
{
    public required string Name { get; set; }
    public required string Email { get; set; }
}