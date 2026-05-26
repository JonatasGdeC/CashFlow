using CashFlow.Domain.Enitites;

namespace CashFlow.Domain.Repositories.CategoriesGoals;

public interface ICategoriesGoalsWriteRepository
{
    Task Add(CategoryGoal categoryGoal);
    void Delete(CategoryGoal categoryGoal);
    void Update(CategoryGoal categoryGoal);
    Task<CategoryGoal?> GetCategoryGoalByIdToUpdate(Guid categoryGoalId, Guid userId);
}