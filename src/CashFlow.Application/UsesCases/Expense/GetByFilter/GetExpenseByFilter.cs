using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Response.Expense;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Services.LoggedUser;

namespace CashFlow.Application.UsesCases.Expense.GetByFilter;

public class GetExpenseByFilter(IExpensesReadRepository readRepository, ILoggedUser loggedUser, IMapper mapper) : IGetExpenseByFilter
{
    public async Task<ResponseGetAllExpensesJson> Execute(RequestFilterJson request)
    {
        Domain.Enitites.User currentUser =  await loggedUser.Get();
        List<Domain.Enitites.Expense>? response = await readRepository.GetFilter(request: request, userId: currentUser.Id);
        
        return new ResponseGetAllExpensesJson
        {
            ListAllExpenses = mapper.Map<List<ResponseExpenseShortJson>>(source: response)
        };
    }
}