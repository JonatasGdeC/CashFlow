using AutoMapper;
using CashFlow.Communication.Response;
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Application.UsesCases.Expense.GetAll;

public class GetAllExpenseUseCase(IExpensesReadRepository writeRepository, IMapper mapper) : IGetAllExpenseUseCase
{
    public async Task<ResponseGetAllExpensesJson> Execute()
    {
        List<Domain.Enitites.Expense>? response = await writeRepository.GetAllExpenses();

        return new ResponseGetAllExpensesJson
        {
            ListAllExpenses = mapper.Map<List<ResponseExpenseShortJson>>(source: response)
        };
    }
}