using CashFlow.Communication.Requests;
using CashFlow.Communication.Requests.CategoryGoal;
using CashFlow.Exception;
using FluentValidation;

namespace CashFlow.Application.UsesCases.CategoryGoal.Register;

public class RegisterCategoryGoalValidator : AbstractValidator<RequestRegisterCategoryGoalJson>
{
    public RegisterCategoryGoalValidator()
    {
        RuleFor(expression: categoryGoal => categoryGoal.TargetAmount)
            .GreaterThan(valueToCompare: 0).WithMessage(errorMessage: ResourceErrorMessage.CATEGORY_GOAL_TARGET_AMOUNT_MUST_BE_GREATER_THAN_ZERO);

        RuleFor(expression: categoryGoal => categoryGoal.Month)
            .InclusiveBetween(from: 1, to: 12).WithMessage(errorMessage: ResourceErrorMessage.CATEGORY_GOAL_MONTH_INVALID);

        RuleFor(expression: categoryGoal => categoryGoal.Year)
            .GreaterThan(valueToCompare: 0).WithMessage(errorMessage: ResourceErrorMessage.CATEGORY_GOAL_YEAR_INVALID);

        RuleFor(expression: categoryGoal => categoryGoal.CategoryId)
            .NotEmpty().WithMessage(errorMessage: ResourceErrorMessage.CATEGORY_ID_REQUIRED);
    }
}
