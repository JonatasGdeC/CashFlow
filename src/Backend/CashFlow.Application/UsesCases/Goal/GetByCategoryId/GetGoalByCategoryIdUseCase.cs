using AutoMapper;
using CashFlow.Communication.Response.Goal;
using CashFlow.Domain.Repositories.CategoriesGoals;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UsesCases.Goal.GetByCategoryId;

public class GetGoalByCategoryIdUseCase(ICategoriesGoalsReadRepository readRepository, IMapper mapper, ILoggedUser loggedUser) : IGetGoalByCategoryIdUseCase
{
    public async Task<ResponseGoalJson> Execute(Guid categoryId)
    {
        Domain.Enitites.User currentUser = await loggedUser.Get();
        Domain.Enitites.Goal? response = await readRepository.GetCategoryGoalByCategoryId(categoryId: categoryId, userId: currentUser.Id);

        if (response != null)
        {
            return mapper.Map<ResponseGoalJson>(source: response);
        }

        throw new NotFoundException(message: ResourceErrorMessage.CATEGORY_GOAL_NOT_FOUND);
    }
}
