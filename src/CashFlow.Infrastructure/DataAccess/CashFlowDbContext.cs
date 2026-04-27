using CashFlow.Domain.Enitites;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess;

public class CashFlowDbContext : DbContext
{
    public DbSet<Expense> Expenses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = "Server=localhost;Database=CashFlowDb;Uid=root;Pwd=Carvalho@123;";
        MySqlServerVersion serverVersion = new(version: new Version(major: 8,minor: 0,build: 46));
        
        optionsBuilder.UseMySql(connectionString: connectionString, serverVersion: serverVersion);
    }
}