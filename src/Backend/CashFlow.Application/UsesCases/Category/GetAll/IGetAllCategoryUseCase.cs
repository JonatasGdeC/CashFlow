using CashFlow.Communication.Enums;
using CashFlow.Communication.Response;
using CashFlow.Communication.Response.Category;

namespace CashFlow.Application.UsesCases.Category.GetAll;

public interface IGetAllCategoryUseCase
{
    Task<ResponseGetAllCategoriesJson> Execute(CategoryType categoryType);
}
