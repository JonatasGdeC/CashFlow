using CashFlow.Domain.Enitites;

namespace CashFlow.Domain.Repositories.Users;

public interface IUsersReadRepository
{
    Task<bool> ExistsUsersWithThisEmail(string email);
    Task<User?> GetUserByEmail(string email);
}