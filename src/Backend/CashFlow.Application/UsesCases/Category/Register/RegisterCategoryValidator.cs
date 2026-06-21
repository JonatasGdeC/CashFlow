using CashFlow.Communication.Requests;
using CashFlow.Communication.Requests.Category;
using CashFlow.Exception;
using FluentValidation;

namespace CashFlow.Application.UsesCases.Category.Register;

public class RegisterCategoryValidator : AbstractValidator<RequestRegisterCategoryJson>
{
    public RegisterCategoryValidator()
    {
        RuleFor(expression: category => category.Name)
            .NotEmpty().WithMessage(errorMessage: ResourceErrorMessage.CATEGORY_NAME_REQUIRED)
            .MaximumLength(maximumLength: 100).WithMessage(errorMessage: ResourceErrorMessage.CATEGORY_NAME_MAXIMUM_LENGTH);

        RuleFor(expression: category => category.Description)
            .MaximumLength(maximumLength: 1000).WithMessage(errorMessage: ResourceErrorMessage.CATEGORY_DESCRIPTION_MAXIMUM_LENGTH);

        RuleFor(expression: category => category.Color)
            .NotEmpty().WithMessage(errorMessage: ResourceErrorMessage.CATEGORY_COLOR_REQUIRED)
            .MaximumLength(maximumLength: 20).WithMessage(errorMessage: ResourceErrorMessage.CATEGORY_COLOR_MAXIMUM_LENGTH);

        RuleFor(expression: category => category.Type)
            .IsInEnum().WithMessage(errorMessage: ResourceErrorMessage.CATEGORY_TYPE_INVALID);
    }
}
