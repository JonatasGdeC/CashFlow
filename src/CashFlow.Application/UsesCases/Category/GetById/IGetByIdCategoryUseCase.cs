using CashFlow.Communication.Response;

namespace CashFlow.Application.UsesCases.Category.GetById;

public interface IGetByIdCategoryUseCase
{
    Task<ResponseCategoryJson> Execute(Guid id);
}
