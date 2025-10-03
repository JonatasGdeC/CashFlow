using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using FluentValidation.Results;

namespace CashFlow.Application.UseCases.Expenses.Update;

public class UpdateExpenseUseCase : IUpdateExpenseUseCase
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IMapper _mapper;
  private readonly IExpensesUpdateOnlyRepository _repository;

  public UpdateExpenseUseCase(IUnitOfWork unitOfWork, IMapper mapper, IExpensesUpdateOnlyRepository repository)
  {
    _unitOfWork = unitOfWork;
    _mapper = mapper;
    _repository = repository;
  }

  public async Task Execute(long id, RequestExpenseJson request)
  {
    Validate(request: request);

    Expense? expense = await _repository.GetById(id: id);

    if (expense == null)
    {
      throw new NotFoundException(message: ResourcesErrorMessages.EXPENSE_NOT_FOUND);
    }

    Expense result = _mapper.Map(source: request, destination: expense);

    _repository.Update(expense: result);
    await _unitOfWork.Commit();
  }

  private void Validate(RequestExpenseJson request)
  {
    ExpenseValidator validator = new ExpenseValidator();
    ValidationResult? result = validator.Validate(instance: request);

    if (result.IsValid == false)
    {
      List<string> errorMessage = result.Errors.Select(selector: f => f.ErrorMessage).ToList();
      throw new ErrorOnValidationException(errorMessages: errorMessage);
    }
  }
}
