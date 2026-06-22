using CashFlow.Communication.Response;
using CashFlow.Communication.Response.Category;

namespace CashFlow.Application.UsesCases.Category.GetById;

public interface IGetByIdCategoryUseCase
{
    Task<ResponseCategoryJson> Execute(Guid id);
}
