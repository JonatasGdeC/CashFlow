using CashFlow.Application.UseCases.Expenses.Register;
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

  [Fact]
  public void ErrorTitleEmpty()
  {
    var validator = new RegisterExpenseValidator();
    var request = RequestRegisterExpenseJsonBuilder.Build();
    request.Title = string.Empty;
    
    var result = validator.Validate(request);
    
    result.IsValid.ShouldBeFalse();
    result.Errors.ShouldSatisfyAllConditions(
      e => e.Count.ShouldBe(1),
      e => e.First().ErrorMessage.ShouldBe(ResourcesErrorMessages.TITLE_REQUIRED)
      );
  }
}