using CashFlow.Communication.Requests;
using CashFlow.Communication.Requests.CategoryGoal;

namespace CashFlow.Application.UsesCases.CategoryGoal.Update;

public interface IUpdateCategoryGoalUseCase
{
    Task Execute(Guid id, RequestRegisterCategoryGoalJson request);
}
