using CashFlow.Communication.Requests;
using CashFlow.Communication.Response;

namespace CashFlow.Application.UsesCases.User.Login;

public interface ILoginUseCase
{
    Task<ResponseRegisterUserJson> Execute(RequestLoginJson request);
}