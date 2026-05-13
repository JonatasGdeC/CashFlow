using CashFlow.Domain.Enitites;
using CashFlow.Domain.Repositories.Users;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess.Repositories;

internal class UserRepository(CashFlowDbContext context)  : IUsersReadRepository, IUsersWriteRepository
{
    public async Task<bool> ExistsUsersWithThisEmail(string email)
    {
        return await context.Users.AnyAsync(predicate: user => user.Email == email);
    }

    public async Task Add(User user)
    {
        await context.Users.AddAsync(entity: user);
    }
}