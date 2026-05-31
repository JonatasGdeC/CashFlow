using CashFlow.Communication.Requests;
using CashFlow.Communication.Response.Expense;

namespace CashFlow.Application.UsesCases.Expense.GetByFilter;

public interface IGetExpenseByFilter
{
    Task<ResponseGetAllExpensesJson> Execute(RequestFilterJson request);
}