using CashFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess;

internal class CashFlowDbContext : DbContext
{
  public CashFlowDbContext(DbContextOptions options) : base(options: options) { }

  public DbSet<Expense> Expenses { get; set; }
}
