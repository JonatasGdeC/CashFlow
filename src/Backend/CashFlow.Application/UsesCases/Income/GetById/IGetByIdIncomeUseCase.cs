using CashFlow.Communication.Response;
using CashFlow.Communication.Response.Income;

namespace CashFlow.Application.UsesCases.Income.GetById;

public interface IGetByIdIncomeUseCase
{
    Task<ResponseIncomeJson> Execute(Guid id);
}
