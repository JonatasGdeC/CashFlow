using AutoMapper;
using CashFlow.Communication.Response;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UsesCases.Expense.GetById;

public class GetByIdExpenseUseCase(IExpensesReadRepository readRepository, IMapper mapper, ILoggedUser loggedUser) : IGetByIdExpenseUseCase
{
    public async Task<ResponseExpenseJson> Execute(Guid id)
    {
        Domain.Enitites.User currentUser = await loggedUser.Get();
        Domain.Enitites.Expense? response = await readRepository.GetExpenseById(expenseId: id, userId: currentUser.Id);

        if (response != null)
        {
            return mapper.Map<ResponseExpenseJson>(source: response);
        }
        
        throw new NotFoundException(message: ResourceErrorMessage.EXPENSE_NOT_FOUND); 
    }
}