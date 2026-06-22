using CashFlow.Communication.Requests;
using CashFlow.Communication.Response;
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

    public void Delete(Expense expense)
    {
        context.Expenses.Remove(entity: expense);
    }

    public void Update(Expense expense)
    {
        context.Expenses.Update(entity: expense);
    }

    public async Task<Expense?> GetExpenseByIdToUpdate(Guid expenseId, Guid userId)
    {
        return await context.Expenses.FirstOrDefaultAsync(predicate: expense => expense.Id == expenseId && expense.UserId == userId);
    }

    public async Task<List<Expense>?> GetAllExpenses(Guid userId)
    {
        return await context.Expenses.AsNoTracking().Where(predicate: expense => expense.UserId == userId).ToListAsync();
    }

    public async Task<Expense?> GetExpenseById(Guid expenseId, Guid userId)
    {
        return await context.Expenses.AsNoTracking().FirstOrDefaultAsync(predicate: expense => expense.Id == expenseId && expense.UserId == userId);
    }

    public async Task<List<Expense>?> GetFilter(RequestFilterJson request, Guid userId)
    {
        DateTime startDate = new(year: request.Date.Year, month: request.Date.Month, day: 1);
        DateTime endDate = new(year: request.Date.Year, month: request.Date.Month, day: DateTime.DaysInMonth(year: request.Date.Year, month: request.Date.Month), hour: 23, minute: 59, second: 59);

        return await context.Expenses.AsNoTracking()
            .Where(predicate: expense => expense.UserId == userId 
                                         && expense.Date >= startDate 
                                         && expense.Date <= endDate 
                                         && (!request.Amount.HasValue || expense.Amount == request.Amount.Value)
                                         && (string.IsNullOrWhiteSpace(request.Title) || expense.Title.Contains(request.Title))
                                         && (!request.CategoryId.HasValue || expense.CategoryId == request.CategoryId.Value))
            .OrderBy(keySelector: expense => expense.Date).ThenBy(keySelector: expense => expense.Title).ToListAsync();
    }

    public async Task<ResponseDashboardJson> GetDashboardExpenses(Guid userId)
    {
        int currentYear = DateTime.UtcNow.Year;

        Dictionary<int, decimal> expensesByMonth = await context.Expenses.AsNoTracking()
            .Where(predicate: expense => expense.UserId == userId && expense.Date.Year == currentYear)
            .GroupBy(keySelector: expense => expense.Date.Month)
            .Select(selector: group => new
            {
                Month = group.Key,
                Amount = group.Sum(expense => expense.Amount)
            }).ToDictionaryAsync(keySelector: x => x.Month, elementSelector: x => x.Amount);

        Dictionary<int, decimal> amountForMonth = new();

        for (int month = 1; month <= 12; month++)
        {
            amountForMonth[key: month] = expensesByMonth.GetValueOrDefault(key: month, defaultValue: 0);
        }

        return new ResponseDashboardJson
        {
            AmountForMonth = amountForMonth,
            TotalAmountForYear = amountForMonth.Values.Sum()
        };
    }
}