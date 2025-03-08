using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Expenses.Register;

public class RegisterExpenseUseCase
{
  public ResponseRegisterExpenseJson Execute(RequestRegisterExpenseJson request)
  {
    Validate(request);
    return new ResponseRegisterExpenseJson();
  }

  private void Validate(RequestRegisterExpenseJson request)
  {
    bool titleIsEmpty = string.IsNullOrWhiteSpace(request.Title);

    if (titleIsEmpty)
    {
      throw new ArgumentException("Title is required.");
    }

    if (request.Amount <= 0)
    {
      throw new ArgumentException("Amount must be greater than zero.");
    }

    var result = DateTime.Compare(request.Date, DateTime.Now);
    if (result > 0)
    {
      throw new ArgumentException("Expense cannot be for the future.");
    }

    bool paymentTypeIsValid = Enum.IsDefined(typeof(PaymentType), request.PaymentType);
    if (!paymentTypeIsValid)
    {
      throw new ArgumentException("Payment type is not valid.");
    }
  }
}
