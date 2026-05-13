using CashFlow.Domain.Repositories.Users;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess.Repositories;

internal class UserRepository(CashFlowDbContext context)  : IUsersReadRepository
{
    public async Task<bool> ExistsUsersWithThisEmail(string email)
    {
        return await context.Users.AnyAsync(predicate: user => user.Email == email);
    }
}