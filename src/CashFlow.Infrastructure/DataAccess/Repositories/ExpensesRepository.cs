using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess.Repositories;

internal class ExpensesRepository : IExpensesReadOnlyRepository, IExpensesWhiteOnlyRepository, IExpensesUpdateOnlyRepository
{
  private readonly CashFlowDbContext _context;

  public ExpensesRepository(CashFlowDbContext dbContext)
  {
    _context = dbContext;
  }

  public async Task Add(Expense expense)
  {
    await _context.Expenses.AddAsync(entity: expense);
  }


  public async Task<List<Expense>> GetAll()
  {
    return await _context.Expenses.AsNoTracking().ToListAsync();
  }

  async Task<Expense?> IExpensesReadOnlyRepository.GetById(long id)
  {
    return await _context.Expenses.AsNoTracking().FirstOrDefaultAsync(predicate: expense => expense.Id == id);
  }

  public async Task<List<Expense>> FilterByMonth(DateOnly date)
  {
    DateTime startDate = new DateTime(year: date.Year, month: date.Month, day: 1).Date;
    int daysInMonth = DateTime.DaysInMonth(year: date.Year, month: date.Month);
    DateTime endDate = new DateTime(year: date.Year, month: date.Month, day: daysInMonth, hour: 23, minute: 59, second: 59);

    return await _context.Expenses.AsNoTracking()
      .Where(predicate: expense => expense.Date >= startDate && expense.Date <= endDate)
      .OrderBy(keySelector: expense => expense.Date)
      .ThenBy(keySelector: expense => expense.Title).ToListAsync();
  }

  async Task<Expense?> IExpensesUpdateOnlyRepository.GetById(long id)
  {
    return await _context.Expenses.FirstOrDefaultAsync(predicate: expense => expense.Id == id);
  }

  public async Task<bool> Delete(long id)
  {
    Expense? result = await _context.Expenses.FirstOrDefaultAsync(predicate: expense => expense.Id == id);

    if (result == null)
    {
      return false;
    }

    _context.Expenses.Remove(entity: result);
    return true;
  }

  public void Update(Expense expense)
  {
    _context.Expenses.Update(entity: expense);
  }
}
