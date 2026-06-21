using CashFlow.Communication.Requests;
using CashFlow.Communication.Requests.User;
using CashFlow.Communication.Response;
using CashFlow.Communication.Response.User;

namespace CashFlow.Application.UsesCases.User.Login;

public interface ILoginUseCase
{
    Task<ResponseRegisterUserJson> Execute(RequestLoginJson request);
}