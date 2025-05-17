namespace CashFlow.Exception.ExceptionBase;

public abstract class CashFlowException : System.Exception
{
  protected CashFlowException(string messagr) : base(messagr)
  {

  }
}
