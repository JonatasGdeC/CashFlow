namespace CashFlow.Communication.Response;

public record ResponseErrorJson
{
    public List<string> ErrorMessages { get; init; }

    public ResponseErrorJson(string errorMessage)
    {
        ErrorMessages = [errorMessage];
    }

    public ResponseErrorJson(List<string> errorMessages)
    {
        ErrorMessages = errorMessages;
    }
}