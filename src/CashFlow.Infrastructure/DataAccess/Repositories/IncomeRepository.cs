using CashFlow.Communication.Requests;
using CashFlow.Communication.Response;
using CashFlow.Domain.Enitites;
using CashFlow.Domain.Repositories.Incomes;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess.Repositories;

public class IncomeRepository(CashFlowDbContext context) : IIncomesReadRepository, IIncomesWriteRepository
{
    public async Task<List<Income>?> GetAllIncomes(Guid userId)
    {
        return await context.Incomes.AsNoTracking().Where(predicate: income => income.UserId == userId).ToListAsync();
    }

    public async Task<Income?> GetIncomeById(Guid incomeId, Guid userId)
    {
        return await context.Incomes.AsNoTracking().FirstOrDefaultAsync(predicate: income => income.Id == incomeId && income.UserId == userId);
    }

    public async Task<List<Income>?> GetFilter(RequestFilterJson request, Guid userId)
    {
        DateTime startDate = new(year: request.Date.Year, month: request.Date.Month, day: 1);
        DateTime endDate = new(year: request.Date.Year, month: request.Date.Month, day: DateTime.DaysInMonth(year: request.Date.Year, month: request.Date.Month), hour: 23, minute: 59, second: 59);

        return await context.Incomes.AsNoTracking()
            .Where(predicate: expense => expense.UserId == userId 
                                         && expense.Date >= startDate 
                                         && expense.Date <= endDate 
                                         && (!request.Amount.HasValue || expense.Amount == request.Amount.Value)
                                         && (string.IsNullOrWhiteSpace(request.Title) || expense.Title.Contains(request.Title))
                                         && (!request.CategoryId.HasValue || expense.CategoryId == request.CategoryId.Value))
            .OrderBy(keySelector: expense => expense.Date).ThenBy(keySelector: expense => expense.Title).ToListAsync();
    }

    public async Task<ResponseDashboardJson> GetDashboardIncomes(Guid userId)
    {
        int currentYear = DateTime.UtcNow.Year;

        Dictionary<int, decimal> expensesByMonth = await context.Incomes.AsNoTracking()
            .Where(predicate: income => income.UserId == userId && income.Date.Year == currentYear)
            .GroupBy(keySelector: income => income.Date.Month)
            .Select(selector: group => new
            {
                Month = group.Key,
                Amount = group.Sum(income => income.Amount)
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

    public async Task Add(Income income)
    {
        await context.Incomes.AddAsync(entity: income);
    }

    public void Delete(Income income)
    {
        context.Incomes.Remove(entity: income);
    }

    public void Update(Income income)
    {
        context.Incomes.Update(entity: income);
    }

    public async Task<Income?> GetIncomeByIdToUpdate(Guid incomeId, Guid userId)
    {
        return await context.Incomes.FirstOrDefaultAsync(predicate: income => income.Id == incomeId && income.UserId == userId);
    }
}