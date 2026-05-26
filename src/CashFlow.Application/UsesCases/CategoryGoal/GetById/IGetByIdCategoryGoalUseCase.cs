using CashFlow.Communication.Response;

namespace CashFlow.Application.UsesCases.CategoryGoal.GetById;

public interface IGetByIdCategoryGoalUseCase
{
    Task<ResponseCategoryGoalJson> Execute(Guid id);
}
