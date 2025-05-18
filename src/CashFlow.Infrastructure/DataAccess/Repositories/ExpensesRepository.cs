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
    await _context.Expenses.AddAsync(expense);
  }


  public async Task<List<Expense>> GetAll()
  {
    return await _context.Expenses.AsNoTracking().ToListAsync();
  }

  async Task<Expense?> IExpensesReadOnlyRepository.GetById(long id)
  {
    return await _context.Expenses.AsNoTracking().FirstOrDefaultAsync(expense => expense.Id == id);
  }

  async Task<Expense?> IExpensesUpdateOnlyRepository.GetById(long id)
  {
    return await _context.Expenses.FirstOrDefaultAsync(expense => expense.Id == id);
  }

  public async Task<bool> Delete(long id)
  {
    Expense? result = await _context.Expenses.FirstOrDefaultAsync(expense => expense.Id == id);

    if (result == null)
    {
      return false;
    }

    _context.Expenses.Remove(result);
    return true;
  }

  public void Update(Expense expense)
  {
    _context.Expenses.Update(expense);
  }
}
