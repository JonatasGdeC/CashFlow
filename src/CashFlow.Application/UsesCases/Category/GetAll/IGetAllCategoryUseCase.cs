using CashFlow.Communication.Enums;
using CashFlow.Communication.Response;

namespace CashFlow.Application.UsesCases.Category.GetAll;

public interface IGetAllCategoryUseCase
{
    Task<ResponseGetAllCategoriesJson> Execute(CategoryType categoryType);
}
