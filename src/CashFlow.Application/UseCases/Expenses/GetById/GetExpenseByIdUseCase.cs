using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;

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

    return _mapper.Map<ResponseExpenseJson>(result);
  }
}
