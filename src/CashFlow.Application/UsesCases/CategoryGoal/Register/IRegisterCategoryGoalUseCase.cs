using CashFlow.Communication.Requests;
using CashFlow.Communication.Response;

namespace CashFlow.Application.UsesCases.CategoryGoal.Register;

public interface IRegisterCategoryGoalUseCase
{
    Task<ResponseRegisterCategoryGoalJson> Execute(RequestRegisterCategoryGoalJson request);
}
