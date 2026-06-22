namespace CashFlow.Exception.ExceptionBase;

public abstract class CashFlowException(string message) : System.Exception(message: message)
{
    public abstract int StatusCode { get; }
    public abstract List<string> GetErrors();
}