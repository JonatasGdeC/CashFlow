using CashFlow.Communication.Requests;

namespace CashFlow.Application.UsesCases.User.UpdatePassword;

public interface IChangePasswordUseCase
{
    Task Execute(RequestChangePasswordJson request);
}