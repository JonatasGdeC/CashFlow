using AutoMapper;
using CashFlow.Communication.Response;
using CashFlow.Domain.Repositories.Categories;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UsesCases.Category.GetById;

public class GetByIdCategoryUseCase(ICategoriesReadRepository readRepository, IMapper mapper, ILoggedUser loggedUser) : IGetByIdCategoryUseCase
{
    public async Task<ResponseCategoryJson> Execute(Guid id)
    {
        Domain.Enitites.User currentUser = await loggedUser.Get();
        Domain.Enitites.Category? response = await readRepository.GetCategoryById(categoryId: id, userId: currentUser.Id);

        if (response != null)
        {
            return mapper.Map<ResponseCategoryJson>(source: response);
        }

        throw new NotFoundException(message: ResourceErrorMessage.CATEGORY_NOT_FOUND);
    }
}
