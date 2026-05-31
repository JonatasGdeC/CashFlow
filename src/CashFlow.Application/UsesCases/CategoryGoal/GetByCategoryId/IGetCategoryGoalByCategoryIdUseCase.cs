using CashFlow.Communication.Response;
using CashFlow.Communication.Response.CategoryGoal;

namespace CashFlow.Application.UsesCases.CategoryGoal.GetByCategoryId;

public interface IGetCategoryGoalByCategoryIdUseCase
{
    Task<ResponseCategoryGoalJson> Execute(Guid categoryId);
}
