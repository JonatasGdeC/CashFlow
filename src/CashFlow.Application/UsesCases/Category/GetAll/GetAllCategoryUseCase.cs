using AutoMapper;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Response;
using CashFlow.Communication.Response.Category;
using CashFlow.Domain.Repositories.Categories;
using CashFlow.Domain.Services.LoggedUser;
using DomainCategoryType = CashFlow.Domain.Enums.CategoryType;

namespace CashFlow.Application.UsesCases.Category.GetAll;

public class GetAllCategoryUseCase(ICategoriesReadRepository readRepository, IMapper mapper, ILoggedUser loggedUser) : IGetAllCategoryUseCase
{
    public async Task<ResponseGetAllCategoriesJson> Execute(CategoryType categoryType)
    {
        Domain.Enitites.User currentUser = await loggedUser.Get();
        List<Domain.Enitites.Category>? response = await readRepository.GetAllCategories(categoryType: (DomainCategoryType)categoryType, userId: currentUser.Id);

        return new ResponseGetAllCategoriesJson
        {
            ListAllCategories = mapper.Map<List<ResponseCategoryShortJson>>(source: response)
        };
    }
}
