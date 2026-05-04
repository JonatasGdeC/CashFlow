using AutoMapper;
using CashFlow.Application.UsesCases.Expense.Register;
using CashFlow.Communication.Requests;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionBase;
using FluentValidation.Results;

namespace CashFlow.Application.UsesCases.Expense.Update;

public class UpdateExpenseUseCase(
    IExpensesReadRepository readRepository,
    IExpensesWriteRepository writeRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IUpdateExpenseUseCase
{
    public async Task Execute(Guid id, RequestRegisterExpenseJson request)
    {
        Validate(request: request);

        Domain.Enitites.Expense? expense = await readRepository.GetExpenseById(id: id);

        if (expense == null)
        {
            throw new NotFoundException(message: "Expense not found");
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
