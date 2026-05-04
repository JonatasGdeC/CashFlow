using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UsesCases.Expense.Delete;

public class DeleteExpenseUseCase(IExpensesReadRepository readRepository, IExpensesWriteRepository writeRepository, IUnitOfWork unitOfWork) : IDeleteExpenseUseCase
{
    public async Task Execute(Guid id)
    {
        Domain.Enitites.Expense? expense = await readRepository.GetExpenseById(id: id);
        
        if (expense == null)
        {
            throw new NotFoundException(message: "Expense not found"); 
        }
        
        await writeRepository.Delete(id: expense.Id);
        await unitOfWork.Commit();
    }
}