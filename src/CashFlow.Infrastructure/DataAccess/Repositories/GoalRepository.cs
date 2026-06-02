using CashFlow.Domain.Enitites;
using CashFlow.Domain.Repositories.CategoriesGoals;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess.Repositories;

public class GoalRepository(CashFlowDbContext context) : ICategoriesGoalsReadRepository, ICategoriesGoalsWriteRepository
{
    public async Task<Goal?> GetCategoryGoalByCategoryId(Guid categoryId, Guid userId)
    {
        return await context.Goals.AsNoTracking().FirstOrDefaultAsync(predicate: c => c.CategoryId == categoryId && c.UserId == userId);
    }

    public async Task<Goal?> GetCategoryGoalById(Guid categoryGoalId, Guid userId)
    {
        return await context.Goals.AsNoTracking().FirstOrDefaultAsync(predicate: c => c.Id == categoryGoalId && c.UserId == userId);
    }

    public async Task Add(Goal goal)
    {
        await context.Goals.AddAsync(entity: goal);
    }

    public void Delete(Goal goal)
    {
        context.Goals.Remove(entity: goal);
    }

    public void Update(Goal goal)
    {
        context.Goals.Update(entity: goal);
    }

    public async Task<Goal?> GetCategoryGoalByIdToUpdate(Guid categoryGoalId, Guid userId)
    {
        return await context.Goals.FirstOrDefaultAsync(predicate: c => c.Id == categoryGoalId && c.UserId == userId);
    }
}
