using CashFlow.Communication.Response.Goal;

namespace CashFlow.Application.UsesCases.Goal.GetByCategoryId;

public interface IGetGoalByCategoryIdUseCase
{
    Task<ResponseGoalJson> Execute(Guid categoryId);
}
