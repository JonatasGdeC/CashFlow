using CashFlow.Communication.Response;

namespace CashFlow.Application.UsesCases.Expense.GetById;

public interface IGetByIdExpenseUseCase
{
    Task<ResponseExpenseJson> Execute(Guid id);
}