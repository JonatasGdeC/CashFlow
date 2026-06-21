using CashFlow.Communication.Response;
using CashFlow.Communication.Response.User;

namespace CashFlow.Application.UsesCases.User.Get;

public interface IGetUserProfileUseCase
{
    Task<ResponseUserProfileJson> Execute();
}