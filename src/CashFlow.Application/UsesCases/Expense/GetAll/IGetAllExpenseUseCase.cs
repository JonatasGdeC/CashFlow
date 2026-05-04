using CashFlow.Communication.Response;

namespace CashFlow.Application.UsesCases.Expense.GetAll;

public interface IGetAllExpenseUseCase
{
    Task<ResponseGetAllExpensesJson> Execute();
}