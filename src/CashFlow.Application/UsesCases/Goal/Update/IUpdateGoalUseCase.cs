using CashFlow.Communication.Requests.Goal;

namespace CashFlow.Application.UsesCases.Goal.Update;

public interface IUpdateGoalUseCase
{
    Task Execute(Guid id, RequestRegisterGoalJson request);
}
