using CashFlow.Communication.Response;
using CashFlow.Communication.Response.Income;

namespace CashFlow.Application.UsesCases.Income.GetAll;

public interface IGetAllIncomeUseCase
{
    Task<ResponseGetAllIncomesJson> Execute();
}
