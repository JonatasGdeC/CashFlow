using CashFlow.Communication.Requests;
using CashFlow.Communication.Requests.Expense;

namespace CashFlow.Application.UsesCases.Expense.Update;

public interface IUpdateExpenseUseCase
{
    Task Execute(Guid id, RequestRegisterExpenseJson request);
}
