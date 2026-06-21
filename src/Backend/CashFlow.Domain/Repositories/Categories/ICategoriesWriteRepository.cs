using CashFlow.Domain.Enitites;

namespace CashFlow.Domain.Repositories.Categories;

public interface ICategoriesWriteRepository
{
    Task Add(Category category);
    void Delete(Category category);
    void Update(Category category);
    Task<Category?> GetCategoryByIdToUpdate(Guid categoryId, Guid userId);
}