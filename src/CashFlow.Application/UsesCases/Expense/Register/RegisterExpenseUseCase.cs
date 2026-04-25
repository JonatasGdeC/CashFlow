using CashFlow.Communication.Requests;
using CashFlow.Communication.Response;

namespace CashFlow.Application.UsesCases.Expense.Register;

public class RegisterExpenseUseCase
{
    public ResponseRegisterExpenseJson Execute(RequestRegisterExpenseJson request)
    {
        return new ResponseRegisterExpenseJson();
    }
}