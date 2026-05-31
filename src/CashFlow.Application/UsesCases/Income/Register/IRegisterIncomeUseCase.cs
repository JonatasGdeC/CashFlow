using CashFlow.Communication.Requests;
using CashFlow.Communication.Requests.Income;
using CashFlow.Communication.Response;
using CashFlow.Communication.Response.Income;

namespace CashFlow.Application.UsesCases.Income.Register;

public interface IRegisterIncomeUseCase
{
    Task<ResponseRegisterIncomeJson> Execute(RequestRegisterIncomeJson request);
}
