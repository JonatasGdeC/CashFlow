namespace CashFlow.Communication.Requests;

public class RequestUpdateUserJson
{
    public required string Name { get; set; }
    public required string Email { get; set; }
}