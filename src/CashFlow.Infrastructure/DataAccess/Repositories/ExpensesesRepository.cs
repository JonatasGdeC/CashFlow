using CashFlow.Domain.Enitites;
using CashFlow.Domain.Repositories.Expenses;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess.Repositories;

internal class ExpensesesRepository(CashFlowDbContext context) : IExpensesWriteRepository, IExpensesReadRepository
{
    public async Task Add(Expense expense)
    {
        await context.Expenses.AddAsync(entity:  expense);
    }

    public async Task Delete(Guid id)
    {
        Expense entity = await context.Expenses.FirstAsync(predicate: expense => expense.Id == id);
        context.Expenses.Remove(entity: entity);
    }

    public async Task<List<Expense>?> GetAllExpenses()
    {
        return await context.Expenses.AsNoTracking().ToListAsync();
    }

    public async Task<Expense?> GetExpenseById(Guid id)
    {
        return await context.Expenses.AsNoTracking().FirstOrDefaultAsync(predicate: expense => expense.Id == id);
    }
}