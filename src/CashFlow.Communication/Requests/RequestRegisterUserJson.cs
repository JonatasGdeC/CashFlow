namespace CashFlow.Communication.Requests;

public class RequestRegisterUserJson : RequestUpdateUserJson
{
    public required string Password { get; set; }
}