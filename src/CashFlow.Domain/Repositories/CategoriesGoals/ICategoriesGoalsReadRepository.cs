using CashFlow.Domain.Enitites;

namespace CashFlow.Domain.Repositories.CategoriesGoals;

public interface ICategoriesGoalsReadRepository
{
    Task<CategoryGoal?> GetCategoryGoalByCategoryId(Guid categoryId, Guid userId);
    Task<CategoryGoal?> GetCategoryGoalById(Guid categoryGoalId, Guid userId);
}
