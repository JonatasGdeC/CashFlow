using CashFlow.Communication.Requests;
using CashFlow.Communication.Requests.CategoryGoal;
using CashFlow.Communication.Response;
using CashFlow.Communication.Response.CategoryGoal;

namespace CashFlow.Application.UsesCases.CategoryGoal.Register;

public interface IRegisterCategoryGoalUseCase
{
    Task<ResponseRegisterCategoryGoalJson> Execute(RequestRegisterCategoryGoalJson request);
}
