using CashFlow.Communication.Response.Goal;

namespace CashFlow.Application.UsesCases.Goal.GetById;

public interface IGetGoalByIdUseCase
{
    Task<ResponseGoalJson> Execute(Guid id);
}
