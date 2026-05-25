using CashFlow.Communication.Requests;

namespace CashFlow.Application.UsesCases.Income.Update;

public interface IUpdateIncomeUseCase
{
    Task Execute(Guid id, RequestRegisterIncomeJson request);
}
