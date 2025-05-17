using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Enums;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Expenses.Register;

public class RegisterExpenseUseCase : IRegisterExpenseUseCase
{
  private readonly IExpensesRepository _repository;
  private readonly IUnitOfWork _unitOfWork;

  public RegisterExpenseUseCase(IExpensesRepository repository, IUnitOfWork unitOfWork)
  {
    _repository = repository;
    _unitOfWork = unitOfWork;
  }

  public async Task<ResponseRegisterExpenseJson> Execute(RequestRegisterExpenseJson request)
  {
    Validate(request);

    var entity = new Expense
    {
      Title = request.Title,
      Description = request.Description,
      Date = request.Date,
      Amount = request.Amount,
      PaymentType = (Domain.Enums.PaymentType)request.PaymentType,
    };

    await _repository.Add(entity);
    await _unitOfWork.Commit();
    return new ResponseRegisterExpenseJson();
  }

  private void Validate(RequestRegisterExpenseJson request)
  {
    var validator = new RegisterExpenseValidator();
    var result = validator.Validate(request);

    if (!result.IsValid)
    {
      var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
      throw new ErrorOnValidationException(errorMessages);
    }
  }
}
