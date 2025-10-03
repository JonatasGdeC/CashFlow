using AutoMapper;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Expenses.Delete;

public class DeleteExpenseUseCase : IDeleteExpenseUseCase
{
  private readonly IExpensesWhiteOnlyRepository _repository;
  private readonly IUnitOfWork _unitOfWork;

  public DeleteExpenseUseCase(IExpensesWhiteOnlyRepository repository, IUnitOfWork unitOfWork)
  {
    _repository = repository;
    _unitOfWork = unitOfWork;
  }

  public async Task Execute(long id)
  {
    bool result = await _repository.Delete(id: id);

    if (result == false)
    {
      throw new NotFoundException(message: ResourcesErrorMessages.EXPENSE_NOT_FOUND);
    }

    await _unitOfWork.Commit();
  }
}
