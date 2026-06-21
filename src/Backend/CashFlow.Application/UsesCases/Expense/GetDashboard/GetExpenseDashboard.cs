using CashFlow.Communication.Response;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Services.LoggedUser;

namespace CashFlow.Application.UsesCases.Expense.GetDashboard;
using Domain.Enitites;

public class GetExpenseDashboard(IExpensesReadRepository readRepository, ILoggedUser loggedUser) : IGetExpenseDashboard
{
    public async Task<ResponseDashboardJson> Execute()
    {
        User currentUser = await loggedUser.Get();
        return await readRepository.GetDashboardExpenses(userId: currentUser.Id);
    }
}