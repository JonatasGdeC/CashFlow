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