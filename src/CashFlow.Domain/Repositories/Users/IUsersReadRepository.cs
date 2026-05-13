namespace CashFlow.Domain.Repositories.Users;

public interface IUsersReadRepository
{
    Task<bool> ExistsUsersWithThisEmail(string email);
}