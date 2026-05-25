using CashFlow.Domain.Enitites;
using CashFlow.Domain.Enums;

namespace CashFlow.Domain.Repositories.Categories;

public interface ICategoriesReadRepository
{
    Task<List<Category>?> GetAllCategories(CategoryType categoryType, Guid userId);
}