using AutoMapper;
using CashFlow.Application.UsesCases.Expense.Register;
using CashFlow.Communication.Requests;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using FluentValidation.Results;

namespace CashFlow.Application.UsesCases.Expense.Update;

public class UpdateExpenseUseCase(
    IExpensesWriteRepository writeRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ILoggedUser loggedUser) : IUpdateExpenseUseCase
{
    public async Task Execute(Guid id, RequestRegisterExpenseJson request)
    {
        Validate(request: request);

        Domain.Enitites.User currentUser = await loggedUser.Get();
        Domain.Enitites.Expense? expense = await writeRepository.GetExpenseByIdToUpdate(expenseId: id, userId: currentUser.Id);

        if (expense == null)
        {
            throw new NotFoundException(message: ResourceErrorMessage.EXPENSE_NOT_FOUND);
        }

        mapper.Map(source: request, destination: expense);
        expense.Id = id;

        writeRepository.Update(expense: expense);
        await unitOfWork.Commit();
    }

    private void Validate(RequestRegisterExpenseJson request)
    {
        RegisterExpenseValidator validator = new();
        ValidationResult result = validator.Validate(instance: request);

        if (!result.IsValid)
        {
            List<string> errors = result.Errors.Select(selector: error => error.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorsMessages: errors);
        }
    }
}
