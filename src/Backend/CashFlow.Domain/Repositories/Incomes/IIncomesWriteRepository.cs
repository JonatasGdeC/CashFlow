using CashFlow.Domain.Enitites;

namespace CashFlow.Domain.Repositories.Incomes;

public interface IIncomesWriteRepository
{
    Task Add(Income income);
    void Delete(Income income);
    void Update(Income income);
    Task<Income?> GetIncomeByIdToUpdate(Guid incomeId, Guid userId);
}
