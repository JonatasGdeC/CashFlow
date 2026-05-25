using CashFlow.Communication.Response;

namespace CashFlow.Application.UsesCases.Income.GetDashboard;

public interface IGetIncomeDashboard
{
    Task<ResponseDashboardJson> Execute();
}
