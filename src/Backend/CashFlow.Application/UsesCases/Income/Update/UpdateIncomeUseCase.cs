using AutoMapper;
using CashFlow.Application.UsesCases.Income.Register;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Requests.Income;
using CashFlow.Domain.Enums;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Categories;
using CashFlow.Domain.Repositories.Incomes;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using FluentValidation.Results;

namespace CashFlow.Application.UsesCases.Income.Update;

public class UpdateIncomeUseCase(
    IIncomesWriteRepository writeRepository,
    ICategoriesReadRepository categoriesReadRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ILoggedUser loggedUser) : IUpdateIncomeUseCase
{
    public async Task Execute(Guid id, RequestRegisterIncomeJson request)
    {
        Domain.Enitites.User currentUser = await loggedUser.Get();
        
        await Validate(request: request, userId: currentUser.Id);
        
        Domain.Enitites.Income? income = await writeRepository.GetIncomeByIdToUpdate(incomeId: id, userId: currentUser.Id);

        if (income == null)
        {
            throw new NotFoundException(message: ResourceErrorMessage.INCOME_NOT_FOUND);
        }

        mapper.Map(source: request, destination: income);
        income.Id = id;

        writeRepository.Update(income: income);
        await unitOfWork.Commit();
    }

    private async Task Validate(RequestRegisterIncomeJson request, Guid userId)
    {
        RegisterIncomeValidator validator = new();
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
