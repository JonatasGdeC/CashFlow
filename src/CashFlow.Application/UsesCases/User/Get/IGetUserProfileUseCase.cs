using CashFlow.Communication.Response;

namespace CashFlow.Application.UsesCases.User.Get;

public interface IGetUserProfileUseCase
{
    Task<ResponseUserProfileJson> Execute();
}