namespace CashFlow.Communication.Requests.User;

public class RequestRegisterUserJson : RequestUpdateUserJson
{
    public required string Password { get; set; }
}