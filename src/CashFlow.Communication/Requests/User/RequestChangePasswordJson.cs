namespace CashFlow.Communication.Requests.User;

public record RequestChangePasswordJson
{
    public required string OldPassword { get; set; }
    public required string NewPassword { get; set; }
}