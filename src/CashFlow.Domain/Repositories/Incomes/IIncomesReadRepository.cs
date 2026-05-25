using CashFlow.Domain.Enitites;

namespace CashFlow.Domain.Repositories.Incomes;

public interface IIncomesReadRepository
{
    Task<List<Income>?> GetAllIncomes(Guid userId);
    Task<Income?> GetIncomeById(Guid incomeId, Guid userId);
}