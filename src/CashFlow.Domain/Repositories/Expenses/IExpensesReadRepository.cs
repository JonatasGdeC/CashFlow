using CashFlow.Communication.Requests;
using CashFlow.Communication.Response;
using CashFlow.Domain.Enitites;

namespace CashFlow.Domain.Repositories.Expenses;

public interface IExpensesReadRepository
{
    Task<List<Expense>?> GetAllExpenses(Guid userId);
    Task<Expense?> GetExpenseById(Guid expenseId, Guid userId);
    Task<List<Expense>?> GetFilter(RequestFilterJson request, Guid userId);
    Task<ResponseDashboardJson> GetDashboardExpenses(Guid userId);
}