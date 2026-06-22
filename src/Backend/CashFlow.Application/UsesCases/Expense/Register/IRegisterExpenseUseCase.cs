using CashFlow.Communication.Requests;
using CashFlow.Communication.Requests.Expense;
using CashFlow.Communication.Response;
using CashFlow.Communication.Response.Expense;

namespace CashFlow.Application.UsesCases.Expense.Register;

public interface IRegisterExpenseUseCase
{
    Task<ResponseRegisterExpenseJson> Execute(RequestRegisterExpenseJson request);
}