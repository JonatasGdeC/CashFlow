using CashFlow.Application.UsesCases.Expense.Register;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
using FluentValidation.Results;

namespace Validators.Tests.Expense.Register;

public class RegisterExpenseValidatorTests
{
    [Fact]
    public void Success()
    {
        // Arrange
        RegisterExpenseValidator validator = new();
        RequestRegisterExpenseJson request = new()
        {
            Title = "Apple",
            Description = "Description",
            Date = DateTime.Today.AddDays(value: -1),
            Amount = 100,
            PaymentType = PaymentType.Cash
        };

        // Act
        ValidationResult? result = validator.Validate(instance: request);

        // Assert
        Assert.True(condition: result.IsValid);
    }
}