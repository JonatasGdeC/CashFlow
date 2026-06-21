using CashFlow.Communication.Response;
using CashFlow.Communication.Response.Expense;

namespace CashFlow.Application.UsesCases.Expense.GetById;

public interface IGetByIdExpenseUseCase
{
    Task<ResponseExpenseJson> Execute(Guid id);
}