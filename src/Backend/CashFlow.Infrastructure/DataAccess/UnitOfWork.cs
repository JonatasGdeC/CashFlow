using CashFlow.Domain.Repositories;

namespace CashFlow.Infrastructure.DataAccess;

internal class UnitOfWork(CashFlowDbContext context) : IUnitOfWork
{
    public async Task Commit()
    {
        await context.SaveChangesAsync();
    }
}