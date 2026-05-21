using CashFlow.Domain.Enitites;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess;

public class CashFlowDbContext(DbContextOptions options) : DbContext(options: options)
{
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<User> Users { get; set; }
}