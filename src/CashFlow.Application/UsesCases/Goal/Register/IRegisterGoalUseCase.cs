using CashFlow.Communication.Requests.Goal;
using CashFlow.Communication.Response.Goal;

namespace CashFlow.Application.UsesCases.Goal.Register;

public interface IRegisterGoalUseCase
{
    Task<ResponseRegisterGoalJson> Execute(RequestRegisterGoalJson request);
}
