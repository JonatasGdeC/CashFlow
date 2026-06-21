using CashFlow.Communication.Requests;
using CashFlow.Communication.Requests.Income;
using CashFlow.Exception;
using FluentValidation;

namespace CashFlow.Application.UsesCases.Income.Register;

public class RegisterIncomeValidator : AbstractValidator<RequestRegisterIncomeJson>
{
    public RegisterIncomeValidator()
    {
        RuleFor(expression: income => income.Title)
            .NotEmpty().WithMessage(errorMessage: ResourceErrorMessage.INCOME_TITLE_REQUIRED)
            .MaximumLength(maximumLength: 100).WithMessage(errorMessage: ResourceErrorMessage.INCOME_TITLE_MAXIMUM_LENGTH);

        RuleFor(expression: income => income.Description)
            .MaximumLength(maximumLength: 1000).WithMessage(errorMessage: ResourceErrorMessage.INCOME_DESCRIPTION_MAXIMUM_LENGTH);

        RuleFor(expression: income => income.Date)
            .NotEmpty().WithMessage(errorMessage: ResourceErrorMessage.INCOME_DATE_REQUIRED)
            .LessThanOrEqualTo(expression: _ => DateTime.Today).WithMessage(errorMessage: ResourceErrorMessage.INCOME_DATE_CANNOT_BE_IN_THE_FUTURE);

        RuleFor(expression: income => income.Amount)
            .GreaterThan(valueToCompare: 0).WithMessage(errorMessage: ResourceErrorMessage.INCOME_AMOUNT_MUST_BE_GREATER_THAN_ZERO);
    }
}
