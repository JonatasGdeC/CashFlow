using CashFlow.Domain.Enitites;

namespace CashFlow.Domain.Repositories.Users;

public interface IUsersWriteRepository
{
    Task Add(User user);
    void Update(User user);
    void Delete(User user);
}