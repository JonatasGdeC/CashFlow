using CashFlow.Communication.Response;
using CashFlow.Domain.Repositories.Incomes;
using CashFlow.Domain.Services.LoggedUser;

namespace CashFlow.Application.UsesCases.Income.GetDashboard;
using Domain.Enitites;

public class GetIncomeDashboard(IIncomesReadRepository readRepository, ILoggedUser loggedUser) : IGetIncomeDashboard
{
    public async Task<ResponseDashboardJson> Execute()
    {
        User currentUser = await loggedUser.Get();
        return await readRepository.GetDashboardIncomes(userId: currentUser.Id);
    }
}
