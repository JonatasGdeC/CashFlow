using CashFlow.Communication.Requests;
using CashFlow.Communication.Response;

namespace CashFlow.Application.UsesCases.Category.Register;

public interface IRegisterCategoryUseCase
{
    Task<ResponseRegisterCategoryJson> Execute(RequestRegisterCategoryJson request);
}
