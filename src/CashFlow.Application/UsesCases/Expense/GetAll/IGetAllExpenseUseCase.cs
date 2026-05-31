using CashFlow.Communication.Response;
using CashFlow.Communication.Response.Expense;

namespace CashFlow.Application.UsesCases.Expense.GetAll;

public interface IGetAllExpenseUseCase
{
    Task<ResponseGetAllExpensesJson> Execute();
}