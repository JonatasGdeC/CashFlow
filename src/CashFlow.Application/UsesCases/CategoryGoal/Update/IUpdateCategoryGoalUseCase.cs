using CashFlow.Communication.Requests;

namespace CashFlow.Application.UsesCases.CategoryGoal.Update;

public interface IUpdateCategoryGoalUseCase
{
    Task Execute(Guid id, RequestRegisterCategoryGoalJson request);
}
