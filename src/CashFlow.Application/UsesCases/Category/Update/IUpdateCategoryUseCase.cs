using CashFlow.Communication.Requests;

namespace CashFlow.Application.UsesCases.Category.Update;

public interface IUpdateCategoryUseCase
{
    Task Execute(Guid id, RequestRegisterCategoryJson request);
}
