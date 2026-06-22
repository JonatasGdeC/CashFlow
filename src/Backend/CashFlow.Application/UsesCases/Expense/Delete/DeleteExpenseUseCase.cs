using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UsesCases.Expense.Delete;

public class DeleteExpenseUseCase(
    IExpensesWriteRepository writeRepository,
    IUnitOfWork unitOfWork,
    ILoggedUser loggedUser) : IDeleteExpenseUseCase
{
    public async Task Execute(Guid id)
    {
        Domain.Enitites.User currentUser = await loggedUser.Get();
        Domain.Enitites.Expense? expense = await writeRepository.GetExpenseByIdToUpdate(expenseId: id, userId: currentUser.Id);

        if (expense == null)
        {
            throw new NotFoundException(message: ResourceErrorMessage.EXPENSE_NOT_FOUND);
        }

        writeRepository.Delete(expense: expense);
        await unitOfWork.Commit();
    }
}