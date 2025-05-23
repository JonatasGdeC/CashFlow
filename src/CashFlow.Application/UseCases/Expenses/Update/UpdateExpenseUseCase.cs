using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;

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
    Validate(request);

    Expense? expense = await _repository.GetById(id);

    if (expense == null)
    {
      throw new NotFoundException(ResourcesErrorMessages.EXPENSE_NOT_FOUND);
    }

    Expense result = _mapper.Map(request, expense);

    _repository.Update(result);
    await _unitOfWork.Commit();
  }

  private void Validate(RequestExpenseJson request)
  {
    var validator = new ExpenseValidator();
    var result = validator.Validate(request);

    if (result.IsValid == false)
    {
      List<string> errorMessage = result.Errors.Select(f => f.ErrorMessage).ToList();
      throw new ErrorOnValidationException(errorMessage);
    }
  }
}
