using AutoMapper;
using CashFlow.Communication.Response;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UsesCases.Expense.GetById;

public class GetByIdExpenseUseCase(IExpensesReadRepository readRepository, IMapper mapper) : IGetByIdExpenseUseCase
{
    public async Task<ResponseExpenseJson> Execute(Guid id)
    {
        Domain.Enitites.Expense? response = await readRepository.GetExpenseById(id: id);

        if (response != null)
        {
            return mapper.Map<ResponseExpenseJson>(source: response);
        }
        
        throw new NotFoundException(message: "Expense not found"); 
    }
}