using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Expenses.GetById;

public class GetExpenseByIdUseCase : IGetExpenseByIdUseCase
{
  private readonly IExpensesRepository _repository;
  private readonly IMapper _mapper;

  public GetExpenseByIdUseCase(IExpensesRepository repository, IMapper mapper)
  {
    _repository = repository;
    _mapper = mapper;
  }

  public async Task<ResponseExpenseJson> Execute(long id)
  {
    Expense? result = await _repository.GetById(id);

    if (result == null)
    {
      throw new NotFoundException(ResourcesErrorMessages.EXPENSE_NOT_FOUND);
    }

    return _mapper.Map<ResponseExpenseJson>(result);
  }
}
