using CashFlow.Domain.Enitites;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess;

public class CashFlowDbContext(DbContextOptions options) : DbContext(options: options)
{
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Goal> Goals { get; set; }
    public DbSet<Income> Incomes { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder: modelBuilder);

        modelBuilder.Entity<Expense>()
            .HasOne(navigationExpression: expense => expense.Category)
            .WithMany()
            .HasForeignKey(foreignKeyExpression: e => e.CategoryId)
            .OnDelete(deleteBehavior: DeleteBehavior.SetNull);

        modelBuilder.Entity<Income>()
            .HasOne(navigationExpression: income => income.Category)
            .WithMany()
            .HasForeignKey(foreignKeyExpression: i => i.CategoryId)
            .OnDelete(deleteBehavior: DeleteBehavior.SetNull);

        modelBuilder.Entity<Goal>()
            .HasOne(navigationExpression: category => category.Category)
            .WithMany()
            .HasForeignKey(foreignKeyExpression: cg => cg.CategoryId)
            .OnDelete(deleteBehavior: DeleteBehavior.Cascade);
    }
}