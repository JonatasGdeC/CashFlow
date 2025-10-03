using CashFlow.Communication.Requests;
using CashFlow.Exception;
using FluentValidation;

namespace CashFlow.Application.UseCases.Expenses;

public class ExpenseValidator : AbstractValidator<RequestExpenseJson>
{
  public ExpenseValidator()
  {
    RuleFor(expression: expense => expense.Title).NotEmpty().WithMessage(errorMessage: ResourcesErrorMessages.TITLE_REQUIRED);
    RuleFor(expression: expense => expense.Amount).GreaterThan(valueToCompare: 0).WithMessage(errorMessage: ResourcesErrorMessages.AMOUNT_MUST_BE_GREATER_THAN_ZERO);
    RuleFor(expression: expense => expense.Date).LessThanOrEqualTo(valueToCompare: DateTime.Now).WithMessage(errorMessage: ResourcesErrorMessages.EXPENSES_CANNOT_FOR_THE_FUTURE);
    RuleFor(expression: expense => expense.PaymentType).IsInEnum().WithMessage(errorMessage: ResourcesErrorMessages.PAYMENT_TYPE_INVALID);
  }
}
