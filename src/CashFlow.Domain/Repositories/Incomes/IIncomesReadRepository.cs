using CashFlow.Communication.Requests;
using CashFlow.Communication.Response;
using CashFlow.Domain.Enitites;

namespace CashFlow.Domain.Repositories.Incomes;

public interface IIncomesReadRepository
{
    Task<List<Income>?> GetAllIncomes(Guid userId);
    Task<Income?> GetIncomeById(Guid incomeId, Guid userId);
    Task<List<Income>?> GetFilter(RequestFilterJson request, Guid userId);
    Task<ResponseDashboardJson> GetDashboardIncomes(Guid userId);
}