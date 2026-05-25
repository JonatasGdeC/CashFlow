using CashFlow.Communication.Response;

namespace CashFlow.Application.UsesCases.Income.GetAll;

public interface IGetAllIncomeUseCase
{
    Task<ResponseGetAllIncomesJson> Execute();
}
