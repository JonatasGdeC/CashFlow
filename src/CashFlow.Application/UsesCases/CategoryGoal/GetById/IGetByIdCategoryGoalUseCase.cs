using CashFlow.Communication.Response;
using CashFlow.Communication.Response.CategoryGoal;

namespace CashFlow.Application.UsesCases.CategoryGoal.GetById;

public interface IGetByIdCategoryGoalUseCase
{
    Task<ResponseCategoryGoalJson> Execute(Guid id);
}
