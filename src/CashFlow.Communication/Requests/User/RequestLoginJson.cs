namespace CashFlow.Communication.Requests.User;

public class RequestLoginJson
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}