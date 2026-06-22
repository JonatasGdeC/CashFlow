using CashFlow.Domain.Enitites;

namespace CashFlow.Domain.Repositories.Expenses;

public interface IExpensesWriteRepository
{
    Task Add(Expense expense);
    void Delete(Expense expense);
    void Update(Expense expense);
    Task<Expense?> GetExpenseByIdToUpdate(Guid expenseId, Guid userId);
}
