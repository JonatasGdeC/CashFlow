using CashFlow.Communication.Requests;
using CashFlow.Communication.Requests.User;

namespace CashFlow.Application.UsesCases.User.Update;

public interface IUpdateUserUseCase
{
    Task Execute(RequestUpdateUserJson request);
}