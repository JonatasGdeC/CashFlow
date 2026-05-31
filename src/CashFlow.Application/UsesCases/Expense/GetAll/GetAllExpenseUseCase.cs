using AutoMapper;
using CashFlow.Communication.Response;
using CashFlow.Communication.Response.Expense;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Services.LoggedUser;

namespace CashFlow.Application.UsesCases.Expense.GetAll;

public class GetAllExpenseUseCase(IExpensesReadRepository readRepository, IMapper mapper, ILoggedUser loggedUser) : IGetAllExpenseUseCase
{
    public async Task<ResponseGetAllExpensesJson> Execute()
    {
        Domain.Enitites.User currentUser = await loggedUser.Get();
        List<Domain.Enitites.Expense>? response = await readRepository.GetAllExpenses(userId: currentUser.Id);

        return new ResponseGetAllExpensesJson
        {
            ListAllExpenses = mapper.Map<List<ResponseExpenseShortJson>>(source: response)
        };
    }
}