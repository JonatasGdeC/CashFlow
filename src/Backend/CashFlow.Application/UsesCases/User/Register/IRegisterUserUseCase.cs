using CashFlow.Communication.Requests;
using CashFlow.Communication.Requests.User;
using CashFlow.Communication.Response;
using CashFlow.Communication.Response.User;

namespace CashFlow.Application.UsesCases.User.Register;

public interface IRegisterUserUseCase
{
    Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson request);
}