namespace CashFlow.Communication.Requests.User;

public record RequestRegisterUserJson : RequestUpdateUserJson
{
    public required string Password { get; set; }
}