using CashFlow.Application.UsesCases.User.Register;
using CashFlow.Communication.Requests;
using CashFlow.Exception;
using CommomTestsUtilies.Requests;
using FluentAssertions;
using FluentValidation.Results;

namespace Validators.Tests.User.Register;

public class RegisterUserValidatorTests
{
    [Fact]
    public void Success()
    {
        RegisterUserValidator validator = new();
        RequestRegisterUserJson request = RequestRegisterUserJsonBuilder.Build();

        ValidationResult? result = validator.Validate(instance: request);

        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData(data: "")]
    [InlineData(data: " ")]
    [InlineData(data: null)]
    public void Error_Name_Empty(string name)
    {
        RegisterUserValidator validator = new();
        RequestRegisterUserJson request = RequestRegisterUserJsonBuilder.Build();
        request.Name = name;

        ValidationResult? result = validator.Validate(instance: request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(predicate: e => e.ErrorMessage.Equals(ResourceErrorMessage.USER_NAME_REQUIRED));
    }

    [Theory]
    [InlineData("")]
    [InlineData("      ")]
    [InlineData(data: null)]
    public void Error_Email_Empty(string email)
    {
        RegisterUserValidator validator = new();
        RequestRegisterUserJson request = RequestRegisterUserJsonBuilder.Build();
        request.Email = email;

        ValidationResult? result = validator.Validate(instance: request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(predicate: e => e.ErrorMessage.Equals(ResourceErrorMessage.USER_EMAIL_REQUIRED));
    }

    [Fact]
    public void Error_Email_Invalid()
    {
        RegisterUserValidator validator = new();
        RequestRegisterUserJson request = RequestRegisterUserJsonBuilder.Build();
        string newEmail = request.Email.Replace(oldChar: '@', newChar: '.');
        request.Email = newEmail;

        ValidationResult? result = validator.Validate(instance: request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(predicate: e => e.ErrorMessage.Equals(ResourceErrorMessage.USER_EMAIL_VALID));
    }
}