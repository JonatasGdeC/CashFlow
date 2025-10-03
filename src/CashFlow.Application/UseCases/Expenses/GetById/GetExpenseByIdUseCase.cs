using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Expenses.GetById;

public class GetExpenseByIdUseCase : IGetExpenseByIdUseCase
{
  private readonly IExpensesReadOnlyRepository _repository;
  private readonly IMapper _mapper;

  public GetExpenseByIdUseCase(IExpensesReadOnlyRepository repository, IMapper mapper)
  {
    _repository = repository;
    _mapper = mapper;
  }

  public async Task<ResponseExpenseJson> Execute(long id)
  {
    Expense? result = await _repository.GetById(id: id);

    if (result == null)
    {
      throw new NotFoundException(message: ResourcesErrorMessages.EXPENSE_NOT_FOUND);
    }

    return _mapper.Map<ResponseExpenseJson>(source: result);
  }
}
