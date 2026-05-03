using CashFlow.Communication.Requests;
using CashFlow.Communication.Response;

namespace CashFlow.Application.UsesCases.Expense.Register;

public interface IRegisterExpenseUseCase
{
    ResponseRegisterExpenseJson Execute(RequestRegisterExpenseJson request);
}