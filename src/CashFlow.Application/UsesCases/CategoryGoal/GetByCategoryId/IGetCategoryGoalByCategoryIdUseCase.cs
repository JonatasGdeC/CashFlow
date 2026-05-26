using CashFlow.Communication.Response;

namespace CashFlow.Application.UsesCases.CategoryGoal.GetByCategoryId;

public interface IGetCategoryGoalByCategoryIdUseCase
{
    Task<ResponseCategoryGoalJson> Execute(Guid categoryId);
}
