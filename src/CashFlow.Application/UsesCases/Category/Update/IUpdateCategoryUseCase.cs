using CashFlow.Communication.Requests;
using CashFlow.Communication.Requests.Category;

namespace CashFlow.Application.UsesCases.Category.Update;

public interface IUpdateCategoryUseCase
{
    Task Execute(Guid id, RequestRegisterCategoryJson request);
}
