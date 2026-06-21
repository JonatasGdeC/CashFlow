using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Categories;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UsesCases.Category.Delete;

public class DeleteCategoryUseCase(
    ICategoriesWriteRepository writeRepository,
    IUnitOfWork unitOfWork,
    ILoggedUser loggedUser) : IDeleteCategoryUseCase
{
    public async Task Execute(Guid id)
    {
        Domain.Enitites.User currentUser = await loggedUser.Get();
        Domain.Enitites.Category? category = await writeRepository.GetCategoryByIdToUpdate(categoryId: id, userId: currentUser.Id);

        if (category == null)
        {
            throw new NotFoundException(message: ResourceErrorMessage.CATEGORY_NOT_FOUND);
        }

        writeRepository.Delete(category: category);
        await unitOfWork.Commit();
    }
}
