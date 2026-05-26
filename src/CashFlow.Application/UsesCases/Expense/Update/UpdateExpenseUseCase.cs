using AutoMapper;
using CashFlow.Application.UsesCases.Expense.Register;
using CashFlow.Communication.Requests;
using CashFlow.Domain.Enums;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Categories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using FluentValidation.Results;

namespace CashFlow.Application.UsesCases.Expense.Update;

public class UpdateExpenseUseCase(
    IExpensesWriteRepository writeRepository,
    ICategoriesReadRepository categoriesReadRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ILoggedUser loggedUser) : IUpdateExpenseUseCase
{
    public async Task Execute(Guid id, RequestRegisterExpenseJson request)
    {
        Domain.Enitites.User currentUser = await loggedUser.Get();
        
        await Validate(request: request, userId: currentUser.Id);
        
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

    private async Task Validate(RequestRegisterExpenseJson request, Guid userId)
    {
        RegisterExpenseValidator validator = new();
        ValidationResult result = validator.Validate(instance: request);

        if (request.CategoryId.HasValue)
        {
           Domain.Enitites.Category? category = await categoriesReadRepository.GetCategoryById(categoryId: request.CategoryId.Value, userId: userId);
           if (category == null || category.Type != CategoryType.Expense)
           {
               result.Errors.Add(item: new ValidationFailure(propertyName: string.Empty, errorMessage: ResourceErrorMessage.CATEGORY_NOT_FOUND));
               return;
           }
        }

        if (!result.IsValid)
        {
            List<string> errors = result.Errors.Select(selector: error => error.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorsMessages: errors);
        }
    }
}
