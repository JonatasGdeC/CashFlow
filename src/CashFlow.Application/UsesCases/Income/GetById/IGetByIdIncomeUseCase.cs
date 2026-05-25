using CashFlow.Communication.Response;

namespace CashFlow.Application.UsesCases.Income.GetById;

public interface IGetByIdIncomeUseCase
{
    Task<ResponseIncomeJson> Execute(Guid id);
}
