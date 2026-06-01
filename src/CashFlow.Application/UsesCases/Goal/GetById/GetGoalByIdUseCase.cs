using AutoMapper;
using CashFlow.Communication.Response.Goal;
using CashFlow.Domain.Repositories.CategoriesGoals;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UsesCases.Goal.GetById;

public class GetGoalByIdUseCase(ICategoriesGoalsReadRepository readRepository, IMapper mapper, ILoggedUser loggedUser) : IGetGoalByIdUseCase
{
    public async Task<ResponseGoalJson> Execute(Guid id)
    {
        Domain.Enitites.User currentUser = await loggedUser.Get();
        Domain.Enitites.Goal? response = await readRepository.GetCategoryGoalById(categoryGoalId: id, userId: currentUser.Id);

        if (response != null)
        {
            return mapper.Map<ResponseGoalJson>(source: response);
        }

        throw new NotFoundException(message: ResourceErrorMessage.CATEGORY_GOAL_NOT_FOUND);
    }
}
