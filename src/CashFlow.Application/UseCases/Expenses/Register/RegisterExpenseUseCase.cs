using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionBase;
using FluentValidation.Results;

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
    Validate(request: request);
    Expense? entity = _mapper.Map<Expense>(source: request);
    await _repository.Add(expense: entity);
    await _unitOfWork.Commit();
    return _mapper.Map<ResponseRegisterExpenseJson>(source: entity);
  }

  private void Validate(RequestExpenseJson request)
  {
    ExpenseValidator validator = new ExpenseValidator();
    ValidationResult? result = validator.Validate(instance: request);

    if (!result.IsValid)
    {
      List<string> errorMessages = result.Errors.Select(selector: f => f.ErrorMessage).ToList();
      throw new ErrorOnValidationException(errorMessages: errorMessages);
    }
  }
}
