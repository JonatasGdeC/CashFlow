using CashFlow.Communication.Requests;
using CashFlow.Communication.Requests.Income;

namespace CashFlow.Application.UsesCases.Income.Update;

public interface IUpdateIncomeUseCase
{
    Task Execute(Guid id, RequestRegisterIncomeJson request);
}
