using CashFlow.Domain.Enitites;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess;

internal class CashFlowDbContext(DbContextOptions options) : DbContext(options: options)
{
    public DbSet<Expense> Expenses { get; set; }
}