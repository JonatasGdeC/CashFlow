using CashFlow.Communication.Requests;
using CashFlow.Communication.Requests.Category;
using CashFlow.Communication.Response;
using CashFlow.Communication.Response.Category;

namespace CashFlow.Application.UsesCases.Category.Register;

public interface IRegisterCategoryUseCase
{
    Task<ResponseRegisterCategoryJson> Execute(RequestRegisterCategoryJson request);
}
