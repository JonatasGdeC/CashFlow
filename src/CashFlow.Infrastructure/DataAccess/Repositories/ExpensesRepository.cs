using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess.Repositories;

internal class ExpensesRepository : IExpensesRepository
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

  public async Task<Expense?> GetById(long id)
  {
    return await _context.Expenses.AsNoTracking().FirstOrDefaultAsync(expense => expense.Id == id);
  }
}
