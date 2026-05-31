using AutoMapper;
using CashFlow.Communication.Response;
using CashFlow.Communication.Response.CategoryGoal;
using CashFlow.Domain.Repositories.CategoriesGoals;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UsesCases.CategoryGoal.GetById;

public class GetByIdCategoryGoalUseCase(ICategoriesGoalsReadRepository readRepository, IMapper mapper, ILoggedUser loggedUser) : IGetByIdCategoryGoalUseCase
{
    public async Task<ResponseCategoryGoalJson> Execute(Guid id)
    {
        Domain.Enitites.User currentUser = await loggedUser.Get();
        Domain.Enitites.CategoryGoal? response = await readRepository.GetCategoryGoalById(categoryGoalId: id, userId: currentUser.Id);

        if (response != null)
        {
            return mapper.Map<ResponseCategoryGoalJson>(source: response);
        }

        throw new NotFoundException(message: ResourceErrorMessage.CATEGORY_GOAL_NOT_FOUND);
    }
}
