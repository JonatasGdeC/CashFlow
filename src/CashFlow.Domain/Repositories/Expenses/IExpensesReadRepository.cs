using CashFlow.Domain.Enitites;

namespace CashFlow.Domain.Repositories.Expenses;

public interface IExpensesReadRepository
{
    Task<List<Expense>?> GetAllExpenses();
}