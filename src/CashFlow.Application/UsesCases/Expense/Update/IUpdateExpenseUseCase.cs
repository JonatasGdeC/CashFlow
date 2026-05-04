using CashFlow.Communication.Requests;

namespace CashFlow.Application.UsesCases.Expense.Update;

public interface IUpdateExpenseUseCase
{
    Task Execute(Guid id, RequestRegisterExpenseJson request);
}
