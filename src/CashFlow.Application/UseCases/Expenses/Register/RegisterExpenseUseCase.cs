using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Expenses.Register;

public class RegisterExpenseUseCase : IRegisterExpenseUseCase
{
  private readonly IExpensesWhiteOnlyRepository _repository;
  private readonly IUnitOfWork _unitOfWork;
  private readonly IMapper _mapper;

  public RegisterExpenseUseCase(IExpensesWhiteOnlyRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
  {
    _repository = repository;
    _unitOfWork = unitOfWork;
    _mapper = mapper;
  }

  public async Task<ResponseRegisterExpenseJson> Execute(RequestExpenseJson request)
  {
    Validate(request);
    var entity = _mapper.Map<Expense>(request);
    await _repository.Add(entity);
    await _unitOfWork.Commit();
    return _mapper.Map<ResponseRegisterExpenseJson>(entity);
  }

  private void Validate(RequestExpenseJson request)
  {
    var validator = new ExpenseValidator();
    var result = validator.Validate(request);

    if (!result.IsValid)
    {
      var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
      throw new ErrorOnValidationException(errorMessages);
    }
  }
}
