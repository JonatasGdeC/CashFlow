using CashFlow.Application.UsesCases.Expense.Register;
using CashFlow.Communication.Requests;
using CommomTestsUtilies.Requests;
using FluentValidation.Results;

namespace Validators.Tests.Expense.Register;

public class RegisterExpenseValidatorTests
{
    [Fact]
    public void Success()
    {
        // Arrange
        RegisterExpenseValidator validator = new();
        RequestRegisterExpenseJson request = RequestRegisterExpenseJsonBuilder.Build();

        // Act
        ValidationResult? result = validator.Validate(instance: request);

        // Assert
        Assert.True(condition: result.IsValid);
    }
}