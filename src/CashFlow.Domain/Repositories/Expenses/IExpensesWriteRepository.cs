using CashFlow.Domain.Enitites;

namespace CashFlow.Domain.Repositories.Expenses;

public interface IExpensesWriteRepository
{
    Task Add(Expense expense);
    Task Delete(Guid id);
    void Update(Expense expense);
}
