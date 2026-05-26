using CashFlow.Communication.Response;

namespace CashFlow.Application.UsesCases.Expense.GetDashboard;

public interface IGetExpenseDashboard
{
    Task<ResponseDashboardJson> Execute();
}