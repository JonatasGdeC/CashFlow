using CashFlow.Application.UsesCases.User;
using FluentAssertions;
using FluentValidation.Results;

namespace Validators.Tests.User;

public class PasswordValidatorTests
{
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("a")]
    [InlineData("aa")]
    [InlineData("aaa")]
    [InlineData("aaaa")]
    [InlineData("aaaaa")]
    [InlineData("aaaaaa")]
    [InlineData("aaaaaaa")]
    [InlineData("aaaaaaaa")]
    [InlineData("Aaaaaaaa")]
    [InlineData("Aaaaaaa1")]
    public void Error_Password_Invalid(string password)
    {
        ValidationResult validator = new PasswordValidator().Validate(instance: password);
        validator.IsValid.Should().BeFalse();
    }
}
