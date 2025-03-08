namespace CashFlow.Communication.Responses;

public class ResponseErrorJson(string errorMessage)
{
  public string ErrorMessage { get; set; } = errorMessage;
}
