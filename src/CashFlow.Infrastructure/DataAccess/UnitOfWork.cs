using CashFlow.Domain.Repositories;

namespace CashFlow.Infrastructure.DataAccess;

internal class UnitOfWork : IUnitOfWork
{
  private readonly CashFlowDbContext _context;

  public UnitOfWork(CashFlowDbContext dbContext)
  {
    _context = dbContext;
  }


  public async Task Commit() => await _context.SaveChangesAsync();
}
