using AutoMapper;
using CashFlow.Communication.Response;
using CashFlow.Domain.Repositories.CategoriesGoals;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UsesCases.CategoryGoal.GetByCategoryId;

public class GetCategoryGoalByCategoryIdUseCase(ICategoriesGoalsReadRepository readRepository, IMapper mapper, ILoggedUser loggedUser) : IGetCategoryGoalByCategoryIdUseCase
{
    public async Task<ResponseCategoryGoalJson> Execute(Guid categoryId)
    {
        Domain.Enitites.User currentUser = await loggedUser.Get();
        Domain.Enitites.CategoryGoal? response = await readRepository.GetCategoryGoalByCategoryId(categoryId: categoryId, userId: currentUser.Id);

        if (response != null)
        {
            return mapper.Map<ResponseCategoryGoalJson>(source: response);
        }

        throw new NotFoundException(message: ResourceErrorMessage.CATEGORY_GOAL_NOT_FOUND);
    }
}
