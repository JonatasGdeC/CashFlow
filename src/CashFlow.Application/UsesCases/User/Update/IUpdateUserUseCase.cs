using CashFlow.Communication.Requests;

namespace CashFlow.Application.UsesCases.User.Update;

public interface IUpdateUserUseCase
{
    Task Execute(RequestUpdateUserJson request);
}