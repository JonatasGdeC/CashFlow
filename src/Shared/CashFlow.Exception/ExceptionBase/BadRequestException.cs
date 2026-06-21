using System.Net;

namespace CashFlow.Exception.ExceptionBase;

public class BadRequestException(string message) : CashFlowException(message: message)
{
    public override int StatusCode => (int)HttpStatusCode.BadRequest;

    public override List<string> GetErrors()
    {
        return [Message];
    }
}