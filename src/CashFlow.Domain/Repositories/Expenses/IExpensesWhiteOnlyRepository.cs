using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses;

public interface IExpensesWhiteOnlyRepository
{
  Task Add(Expense expense);
}
