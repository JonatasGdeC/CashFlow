using CashFlow.Communication.Enums;
using CashFlow.Application.UsesCases.Expense.Register;
using CashFlow.Communication.Requests;
using CashFlow.Exception;
using CommomTestsUtilies.Requests;
using FluentAssertions;
using FluentValidation.Results;

namespace Validators.Tests.Expense.Register;

public class RegisterExpenseValidatorTests
{
    [Fact]
    public void Success()
    {
        RegisterExpenseValidator validator = new();
        RequestRegisterExpenseJson request = RequestRegisterExpenseJsonBuilder.Build();
        
        ValidationResult? result = validator.Validate(instance: request);
        
        result.IsValid.Should().BeTrue();
    }
    
    [Theory]
    [InlineData(data: "")]
    [InlineData(data: " ")]
    [InlineData(data: null)]
    public void Error_Title_Empty(string title)
    {
        RegisterExpenseValidator validator = new();
        RequestRegisterExpenseJson request = RequestRegisterExpenseJsonBuilder.Build();
        request.Title = title;
        
        ValidationResult? result = validator.Validate(instance: request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(predicate: e => e.ErrorMessage.Equals(ResourceErrorMessage.EXPENSE_TITLE_REQUIRED));
    }

    [Fact]
    public void Error_Title_Maximum_Length()
    {
        RegisterExpenseValidator validator = new();
        RequestRegisterExpenseJson request = RequestRegisterExpenseJsonBuilder.Build();
        request.Title = new string(c: 'a', count: 101);

        ValidationResult? result = validator.Validate(instance: request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(predicate: e => e.ErrorMessage.Equals(ResourceErrorMessage.EXPENSE_TITLE_MAXIMUM_LENGTH));
    }

    [Fact]
    public void Error_Description_Maximum_Length()
    {
        RegisterExpenseValidator validator = new();
        RequestRegisterExpenseJson request = RequestRegisterExpenseJsonBuilder.Build();
        request.Description = new string(c: 'a', count: 1001);

        ValidationResult? result = validator.Validate(instance: request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(predicate: e => e.ErrorMessage.Equals(ResourceErrorMessage.EXPENSE_DESCRIPTION_MAXIMUM_LENGTH));
    }

    [Fact]
    public void Error_Date_Empty()
    {
        RegisterExpenseValidator validator = new();
        RequestRegisterExpenseJson request = RequestRegisterExpenseJsonBuilder.Build();
        request.Date = default;

        ValidationResult? result = validator.Validate(instance: request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(predicate: e => e.ErrorMessage.Equals(ResourceErrorMessage.EXPENSE_DATE_REQUIRED));
    }

    [Fact]
    public void Error_Date_In_The_Future()
    {
        RegisterExpenseValidator validator = new();
        RequestRegisterExpenseJson request = RequestRegisterExpenseJsonBuilder.Build();
        request.Date = DateTime.Today.AddDays(value: 1);

        ValidationResult? result = validator.Validate(instance: request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(predicate: e => e.ErrorMessage.Equals(ResourceErrorMessage.EXPENSE_DATE_CANNOT_BE_IN_THE_FUTURE));
    }

    [Theory]
    [InlineData(data: 0)]
    [InlineData(data: -1)]
    public void Error_Amount_Invalid(decimal amount)
    {
        RegisterExpenseValidator validator = new();
        RequestRegisterExpenseJson request = RequestRegisterExpenseJsonBuilder.Build();
        request.Amount = amount;

        ValidationResult? result = validator.Validate(instance: request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(predicate: e => e.ErrorMessage.Equals(ResourceErrorMessage.EXPENSE_AMOUNT_MUST_BE_GREATER_THAN_ZERO));
    }

    [Fact]
    public void Error_Payment_Type_Invalid()
    {
        RegisterExpenseValidator validator = new();
        RequestRegisterExpenseJson request = RequestRegisterExpenseJsonBuilder.Build();
        request.PaymentType = (PaymentType)100;

        ValidationResult? result = validator.Validate(instance: request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(predicate: e => e.ErrorMessage.Equals(ResourceErrorMessage.EXPENSE_PAYMENT_TYPE_INVALID));
    }
}
