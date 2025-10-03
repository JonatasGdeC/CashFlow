using CashFlow.Application.UseCases.Expenses;
using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
using CashFlow.Exception;
using CommonTestUtilities.Requests;
using FluentValidation.Results;
using Shouldly;

namespace Validators.Tests.Expenses.Register;

public class ExpenseValidatorTests
{
  [Fact]
  public void Success()
  {
    //Arrange
    ExpenseValidator validator = new ExpenseValidator();
    RequestExpenseJson request = RequestRegisterExpenseJsonBuilder.Build();

    //Act
    ValidationResult? result = validator.Validate(instance: request);

    //Assert
    result.IsValid.ShouldBeTrue();
  }

  [Theory]
  [InlineData(data: "")]
  [InlineData(data: "  ")]
  [InlineData(data: null)]
  public void ErrorTitleEmpty(string title)
  {
    ExpenseValidator validator = new ExpenseValidator();
    RequestExpenseJson request = RequestRegisterExpenseJsonBuilder.Build();
    request.Title = title;
    
    ValidationResult? result = validator.Validate(instance: request);
    
    result.IsValid.ShouldBeFalse();
    result.Errors.ShouldSatisfyAllConditions(conditions: [e => ShouldBeTestExtensions.ShouldBe(actual: e.Count, expected: 1), e => Enumerable.First<ValidationFailure>(source: e).ErrorMessage.ShouldBe(expected: ResourcesErrorMessages.TITLE_REQUIRED)]
      );
  }
  
  [Fact]
  public void ErrorDateFuture()
  {
    ExpenseValidator validator = new ExpenseValidator();
    RequestExpenseJson request = RequestRegisterExpenseJsonBuilder.Build();
    request.Date = DateTime.Now.AddDays(value: 1);
    
    ValidationResult? result = validator.Validate(instance: request);
    
    result.IsValid.ShouldBeFalse();
    result.Errors.ShouldSatisfyAllConditions(conditions: [e => ShouldBeTestExtensions.ShouldBe(actual: e.Count, expected: 1), e => Enumerable.First<ValidationFailure>(source: e).ErrorMessage.ShouldBe(expected: ResourcesErrorMessages.EXPENSES_CANNOT_FOR_THE_FUTURE)]
    );
  }
  
  [Fact]
  public void ErrorPaymentTypeInvalid()
  {
    ExpenseValidator validator = new ExpenseValidator();
    RequestExpenseJson request = RequestRegisterExpenseJsonBuilder.Build();
    request.PaymentType = (PaymentType)700;
    
    ValidationResult? result = validator.Validate(instance: request);
    
    result.IsValid.ShouldBeFalse();
    result.Errors.ShouldSatisfyAllConditions(conditions: [e => ShouldBeTestExtensions.ShouldBe(actual: e.Count, expected: 1), e => Enumerable.First<ValidationFailure>(source: e).ErrorMessage.ShouldBe(expected: ResourcesErrorMessages.PAYMENT_TYPE_INVALID)]
    );
  }
  
  [Theory]
  [InlineData(data: 0)]
  [InlineData(data: -1)]
  public void ErrorAmountInvalid(decimal amount)
  {
    ExpenseValidator validator = new ExpenseValidator();
    RequestExpenseJson request = RequestRegisterExpenseJsonBuilder.Build();
    request.Amount = amount;
    
    ValidationResult? result = validator.Validate(instance: request);
    
    result.IsValid.ShouldBeFalse();
    result.Errors.ShouldSatisfyAllConditions(conditions: [e => ShouldBeTestExtensions.ShouldBe(actual: e.Count, expected: 1), e => Enumerable.First<ValidationFailure>(source: e).ErrorMessage.ShouldBe(expected: ResourcesErrorMessages.AMOUNT_MUST_BE_GREATER_THAN_ZERO)]
    );
  }
}