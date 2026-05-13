using CashFlow.Communication.Requests;
using CashFlow.Exception;
using FluentValidation;

namespace CashFlow.Application.UsesCases.User.Register;

public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
{
    public RegisterUserValidator()
    {
        RuleFor(expression: user => user.Name)
            .NotEmpty()
            .WithMessage(errorMessage: ResourceErrorMessage.USER_NAME_REQUIRED)
            .MinimumLength(minimumLength: 3)
            .WithMessage(errorMessage: ResourceErrorMessage.USER_NAME_MINIMUM_LENGTH);

        RuleFor(expression: user => user.Email)
            .NotEmpty()
            .WithMessage(errorMessage: ResourceErrorMessage.USER_EMAIL_REQUIRED)
            .EmailAddress()
            .WithMessage(errorMessage: ResourceErrorMessage.USER_EMAIL_VALID);
    }
}
