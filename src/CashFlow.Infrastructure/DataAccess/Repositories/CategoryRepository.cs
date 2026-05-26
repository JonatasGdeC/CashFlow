using CashFlow.Domain.Enitites;
using CashFlow.Domain.Enums;
using CashFlow.Domain.Repositories.Categories;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess.Repositories;

public class CategoryRepository(CashFlowDbContext context) : ICategoriesReadRepository, ICategoriesWriteRepository
{
    public async Task<List<Category>?> GetAllCategories(CategoryType categoryType, Guid userId)
    {
        return await context.Categories.AsNoTracking().Where(predicate: category => category.Type == categoryType && category.UserId == userId).ToListAsync();
    }

    public async Task<Category?> GetCategoryById(Guid categoryId, Guid userId)
    {
        return await context.Categories.AsNoTracking().FirstOrDefaultAsync(predicate: category => category.Id == categoryId && category.UserId == userId);
    }

    public async Task<Category?> GetCategoryByTitle(string title, Guid userId)
    {
        return await context.Categories.AsNoTracking().FirstOrDefaultAsync(predicate: category => category.Name.ToLower() == title.ToLower() && category.UserId == userId);
    }

    public async Task Add(Category category)
    {
        await context.AddAsync(entity: category);
    }

    public void Delete(Category category)
    {
        context.Remove(entity: category);
    }

    public void Update(Category category)
    {
        context.Categories.Update(entity: category);
    }

    public async Task<Category?> GetCategoryByIdToUpdate(Guid categoryId, Guid userId)
    {
        return await context.Categories.FirstOrDefaultAsync(predicate: category => category.Id == categoryId && category.UserId == userId);
    }
}
