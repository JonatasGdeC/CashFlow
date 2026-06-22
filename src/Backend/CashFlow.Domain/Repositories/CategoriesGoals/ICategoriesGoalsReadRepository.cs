using CashFlow.Domain.Enitites;

namespace CashFlow.Domain.Repositories.CategoriesGoals;

public interface ICategoriesGoalsReadRepository
{
    Task<Goal?> GetCategoryGoalByCategoryId(Guid categoryId, Guid userId);
    Task<Goal?> GetCategoryGoalById(Guid categoryGoalId, Guid userId);
}
