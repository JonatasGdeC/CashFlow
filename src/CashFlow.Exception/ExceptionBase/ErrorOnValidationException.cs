using System.Net;

namespace CashFlow.Exception.ExceptionBase;

public class ErrorOnValidationException(List<string> errorsMessages) : CashFlowException(message: string.Empty)
{
    public override int StatusCode => (int)HttpStatusCode.BadRequest;

    public override List<string> GetErrors()
    {
        return errorsMessages;
    }
}