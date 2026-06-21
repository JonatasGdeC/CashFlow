using CashFlow.Domain.Enitites;

namespace CashFlow.Domain.Services.LoggedUser;

public interface ILoggedUser
{
    Task<User> Get();
}