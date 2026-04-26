using CashFlow.Communication.Requests;
using CashFlow.Exception;
using FluentValidation;

namespace CashFlow.Application.UsesCases.Expense.Register;

public class RegisterExpenseValidator : AbstractValidator<RequestRegisterExpenseJson>
{
    public RegisterExpenseValidator()
    {
        RuleFor(expression: expense => expense.Title)
            .NotEmpty().WithMessage(errorMessage: ResourceErrorMessage.EXPENSE_TITLE_REQUIRED)
            .MaximumLength(maximumLength: 100).WithMessage(errorMessage: ResourceErrorMessage.EXPENSE_TITLE_MAXIMUM_LENGTH);

        RuleFor(expression: expense => expense.Description)
            .MaximumLength(maximumLength: 1000).WithMessage(errorMessage: ResourceErrorMessage.EXPENSE_DESCRIPTION_MAXIMUM_LENGTH);

        RuleFor(expression: expense => expense.Date)
            .NotEmpty().WithMessage(errorMessage: ResourceErrorMessage.EXPENSE_DATE_REQUIRED)
            .LessThanOrEqualTo(expression: _ => DateTime.Today).WithMessage(errorMessage: ResourceErrorMessage.EXPENSE_DATE_CANNOT_BE_IN_THE_FUTURE);

        RuleFor(expression: expense => expense.Amount)
            .GreaterThan(valueToCompare: 0).WithMessage(errorMessage: ResourceErrorMessage.EXPENSE_AMOUNT_MUST_BE_GREATER_THAN_ZERO);

        RuleFor(expression: expense => expense.PaymentType)
            .IsInEnum().WithMessage(errorMessage: ResourceErrorMessage.EXPENSE_PAYMENT_TYPE_INVALID);
    }
}
