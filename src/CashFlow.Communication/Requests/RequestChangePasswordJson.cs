namespace CashFlow.Communication.Requests;

public class RequestChangePasswordJson
{
    public required string OldPassword { get; set; }
    public required string NewPassword { get; set; }
}