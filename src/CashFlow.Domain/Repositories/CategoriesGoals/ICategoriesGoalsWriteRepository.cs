using CashFlow.Domain.Enitites;

namespace CashFlow.Domain.Repositories.CategoriesGoals;

public interface ICategoriesGoalsWriteRepository
{
    Task Add(Goal goal);
    void Delete(Goal goal);
    void Update(Goal goal);
    Task<Goal?> GetCategoryGoalByIdToUpdate(Guid categoryGoalId, Guid userId);
}