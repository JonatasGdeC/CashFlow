using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.CategoriesGoals;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UsesCases.Goal.Delete;

public class DeleteGoalUseCase(
    ICategoriesGoalsWriteRepository writeRepository,
    IUnitOfWork unitOfWork,
    ILoggedUser loggedUser) : IDeleteGoalUseCase
{
    public async Task Execute(Guid id)
    {
        Domain.Enitites.User currentUser = await loggedUser.Get();
        Domain.Enitites.Goal? categoryGoal = await writeRepository.GetCategoryGoalByIdToUpdate(categoryGoalId: id, userId: currentUser.Id);

        if (categoryGoal == null)
        {
            throw new NotFoundException(message: ResourceErrorMessage.CATEGORY_GOAL_NOT_FOUND);
        }

        writeRepository.Delete(goal: categoryGoal);
        await unitOfWork.Commit();
    }
}
