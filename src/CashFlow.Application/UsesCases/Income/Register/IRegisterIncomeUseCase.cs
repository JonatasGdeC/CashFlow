using CashFlow.Communication.Requests;
using CashFlow.Communication.Response;

namespace CashFlow.Application.UsesCases.Income.Register;

public interface IRegisterIncomeUseCase
{
    Task<ResponseRegisterIncomeJson> Execute(RequestRegisterIncomeJson request);
}
