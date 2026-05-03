using CashFlow.Domain.Enitites;

namespace CashFlow.Domain.Repositories.Expenses;

public interface IExpensesRepository
{
    Task Add(Expense expense);
}