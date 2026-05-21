namespace CashFlow.Communication.Requests;

public class RequestLoginJson
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}