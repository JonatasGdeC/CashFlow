using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;

namespace Validators.Tests.Expenses.Register;

public class RegisterExpenseValidatorTests
{
  [Fact]
  public void Success()
  {
    //Arrange
    var validator = new RegisterExpenseValidator();
    var request = new RequestRegisterExpenseJson
    {
      Title = "Title",
      Description = "Description",
      Date = DateTime.Now.AddDays(-1),
      Amount = 100,
      PaymentType = PaymentType.Cash
    };

    //Act
    var result = validator.Validate(request);

    //Assert
    Assert.True(result.IsValid);
  }
}