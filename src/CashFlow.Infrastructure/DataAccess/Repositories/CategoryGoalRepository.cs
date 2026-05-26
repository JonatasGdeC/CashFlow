using CashFlow.Domain.Enitites;
using CashFlow.Domain.Repositories.CategoriesGoals;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess.Repositories;

public class CategoryGoalRepository(CashFlowDbContext context) : ICategoriesGoalsReadRepository, ICategoriesGoalsWriteRepository
{
    public async Task<CategoryGoal?> GetCategoryGoalByCategoryId(Guid categoryId, Guid userId)
    {
        return await context.CategoriesGoals.AsNoTracking().FirstOrDefaultAsync(predicate: c => c.CategoryId == categoryId && c.UserId == userId);
    }

    public async Task<CategoryGoal?> GetCategoryGoalById(Guid categoryGoalId, Guid userId)
    {
        return await context.CategoriesGoals.AsNoTracking().FirstOrDefaultAsync(predicate: c => c.Id == categoryGoalId && c.UserId == userId);
    }

    public async Task Add(CategoryGoal categoryGoal)
    {
        await context.CategoriesGoals.AddAsync(entity: categoryGoal);
    }

    public void Delete(CategoryGoal categoryGoal)
    {
        context.CategoriesGoals.Remove(entity: categoryGoal);
    }

    public void Update(CategoryGoal categoryGoal)
    {
        context.CategoriesGoals.Update(entity: categoryGoal);
    }

    public async Task<CategoryGoal?> GetCategoryGoalByIdToUpdate(Guid categoryGoalId, Guid userId)
    {
        return await context.CategoriesGoals.FirstOrDefaultAsync(predicate: c => c.Id == categoryGoalId && c.UserId == userId);
    }
}
