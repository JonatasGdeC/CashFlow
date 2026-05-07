using System.Runtime.InteropServices.JavaScript;
using CashFlow.Communication.Requests;
using CashFlow.Domain.Enitites;
using CashFlow.Domain.Repositories.Expenses;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess.Repositories;

internal class ExpensesesRepository(CashFlowDbContext context) : IExpensesWriteRepository, IExpensesReadRepository
{
    public async Task Add(Expense expense)
    {
        await context.Expenses.AddAsync(entity: expense);
    }

    public async Task Delete(Guid id)
    {
        Expense entity = await context.Expenses.FirstAsync(predicate: expense => expense.Id == id);
        context.Expenses.Remove(entity: entity);
    }

    public void Update(Expense expense)
    {
        context.Expenses.Update(entity: expense);
    }

    public async Task<List<Expense>?> GetAllExpenses()
    {
        return await context.Expenses.AsNoTracking().ToListAsync();
    }

    public async Task<Expense?> GetExpenseById(Guid id)
    {
        return await context.Expenses.AsNoTracking().FirstOrDefaultAsync(predicate: expense => expense.Id == id);
    }

    public async Task<List<Expense>?> GetFilter(RequestInformationReportJson request)
    {
        DateTime startDate = new(year: request.Date.Year, month: request.Date.Month, day: 1);
        DateTime endDate = new(year: request.Date.Year, month: request.Date.Month, day: DateTime.DaysInMonth(year: request.Date.Year, month: request.Date.Month), hour: 23, minute: 59, second: 59);

        return await context.Expenses.AsNoTracking()
            .Where(predicate: expense => expense.Date >= startDate && expense.Date <= endDate)
            .OrderBy(keySelector: expense => expense.Date).ThenBy(keySelector: expense => expense.Title).ToListAsync();
    }
}