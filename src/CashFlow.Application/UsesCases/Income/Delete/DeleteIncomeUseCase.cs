using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Incomes;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UsesCases.Income.Delete;

public class DeleteIncomeUseCase(IIncomesWriteRepository writeRepository, IUnitOfWork unitOfWork, ILoggedUser loggedUser) : IDeleteIncomeUseCase
{
    public async Task Execute(Guid id)
    {
        Domain.Enitites.User currentUser = await loggedUser.Get();
        Domain.Enitites.Income? income = await writeRepository.GetIncomeByIdToUpdate(incomeId: id, userId: currentUser.Id);

        if (income == null)
        {
            throw new NotFoundException(message: ResourceErrorMessage.INCOME_NOT_FOUND);
        }

        writeRepository.Delete(income: income);
        await unitOfWork.Commit();
    }
}
