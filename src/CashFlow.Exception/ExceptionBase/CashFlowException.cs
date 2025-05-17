namespace CashFlow.Exception.ExceptionBase;

public abstract class CashFlowException : System.Exception
{
  protected CashFlowException(string messagr) : base(messagr)
  {

  }

  public abstract int StatusCode { get; }
  public abstract List<string> GetErrors();
}
