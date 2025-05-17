using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses;

public interface IExpensesWhiteOnlyRepository
{
  Task Add(Expense expense);
  /// <summary>
  /// This function returns TRUE if the deletion was successful otherwise returns false
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  Task<bool> Delete(long id);
}
