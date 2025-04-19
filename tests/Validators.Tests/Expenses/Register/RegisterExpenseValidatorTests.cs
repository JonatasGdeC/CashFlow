using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Enums;
using CashFlow.Exception;
using CommonTestUtilities.Requests;
using Shouldly;

namespace Validators.Tests.Expenses.Register;

public class RegisterExpenseValidatorTests
{
  [Fact]
  public void Success()
  {
    //Arrange
    var validator = new RegisterExpenseValidator();
    var request = RequestRegisterExpenseJsonBuilder.Build();

    //Act
    var result = validator.Validate(request);

    //Assert
    result.IsValid.ShouldBeTrue();
  }

  [Theory]
  [InlineData("")]
  [InlineData("  ")]
  [InlineData(null)]
  public void ErrorTitleEmpty(string title)
  {
    var validator = new RegisterExpenseValidator();
    var request = RequestRegisterExpenseJsonBuilder.Build();
    request.Title = title;
    
    var result = validator.Validate(request);
    
    result.IsValid.ShouldBeFalse();
    result.Errors.ShouldSatisfyAllConditions(
      e => e.Count.ShouldBe(1),
      e => e.First().ErrorMessage.ShouldBe(ResourcesErrorMessages.TITLE_REQUIRED)
      );
  }
  
  [Fact]
  public void ErrorDateFuture()
  {
    var validator = new RegisterExpenseValidator();
    var request = RequestRegisterExpenseJsonBuilder.Build();
    request.Date = DateTime.Now.AddDays(1);
    
    var result = validator.Validate(request);
    
    result.IsValid.ShouldBeFalse();
    result.Errors.ShouldSatisfyAllConditions(
      e => e.Count.ShouldBe(1),
      e => e.First().ErrorMessage.ShouldBe(ResourcesErrorMessages.EXPENSES_CANNOT_FOR_THE_FUTURE)
    );
  }
  
  [Fact]
  public void ErrorPaymentTypeInvalid()
  {
    var validator = new RegisterExpenseValidator();
    var request = RequestRegisterExpenseJsonBuilder.Build();
    request.PaymentType = (PaymentType)700;
    
    var result = validator.Validate(request);
    
    result.IsValid.ShouldBeFalse();
    result.Errors.ShouldSatisfyAllConditions(
      e => e.Count.ShouldBe(1),
      e => e.First().ErrorMessage.ShouldBe(ResourcesErrorMessages.PAYMENT_TYPE_INVALID)
    );
  }
  
  [Theory]
  [InlineData(0)]
  [InlineData(-1)]
  public void ErrorAmountInvalid(decimal amount)
  {
    var validator = new RegisterExpenseValidator();
    var request = RequestRegisterExpenseJsonBuilder.Build();
    request.Amount = amount;
    
    var result = validator.Validate(request);
    
    result.IsValid.ShouldBeFalse();
    result.Errors.ShouldSatisfyAllConditions(
      e => e.Count.ShouldBe(1),
      e => e.First().ErrorMessage.ShouldBe(ResourcesErrorMessages.AMOUNT_MUST_BE_GREATER_THAN_ZERO)
    );
  }
}