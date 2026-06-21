using CashFlow.Communication.Requests;
using CashFlow.Communication.Requests.User;

namespace CashFlow.Application.UsesCases.User.UpdatePassword;

public interface IChangePasswordUseCase
{
    Task Execute(RequestChangePasswordJson request);
}