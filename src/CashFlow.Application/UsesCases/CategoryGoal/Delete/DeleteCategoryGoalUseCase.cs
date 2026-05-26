using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.CategoriesGoals;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UsesCases.CategoryGoal.Delete;

public class DeleteCategoryGoalUseCase(
    ICategoriesGoalsWriteRepository writeRepository,
    IUnitOfWork unitOfWork,
    ILoggedUser loggedUser) : IDeleteCategoryGoalUseCase
{
    public async Task Execute(Guid id)
    {
        Domain.Enitites.User currentUser = await loggedUser.Get();
        Domain.Enitites.CategoryGoal? categoryGoal = await writeRepository.GetCategoryGoalByIdToUpdate(categoryGoalId: id, userId: currentUser.Id);

        if (categoryGoal == null)
        {
            throw new NotFoundException(message: ResourceErrorMessage.CATEGORY_GOAL_NOT_FOUND);
        }

        writeRepository.Delete(categoryGoal: categoryGoal);
        await unitOfWork.Commit();
    }
}
