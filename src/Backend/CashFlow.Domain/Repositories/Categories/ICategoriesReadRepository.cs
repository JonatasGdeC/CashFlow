using CashFlow.Domain.Enitites;
using CashFlow.Domain.Enums;

namespace CashFlow.Domain.Repositories.Categories;

public interface ICategoriesReadRepository
{
    Task<List<Category>?> GetAllCategories(CategoryType categoryType, Guid userId);
    Task<Category?> GetCategoryById(Guid categoryId, Guid userId);
    Task<Category?> GetCategoryByTitle(string title, Guid userId);
}
