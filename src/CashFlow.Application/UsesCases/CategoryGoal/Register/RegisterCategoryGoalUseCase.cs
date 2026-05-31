using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Requests.CategoryGoal;
using CashFlow.Communication.Response;
using CashFlow.Communication.Response.CategoryGoal;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Categories;
using CashFlow.Domain.Repositories.CategoriesGoals;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using FluentValidation.Results;

namespace CashFlow.Application.UsesCases.CategoryGoal.Register;

public class RegisterCategoryGoalUseCase(
    ICategoriesGoalsWriteRepository writeRepository,
    ICategoriesGoalsReadRepository readRepository,
    ICategoriesWriteRepository categoriesWriteRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ILoggedUser loggedUser) : IRegisterCategoryGoalUseCase
{
    public async Task<ResponseRegisterCategoryGoalJson> Execute(RequestRegisterCategoryGoalJson request)
    {
        Domain.Enitites.User currentUser = await loggedUser.Get();
        await Validate(request: request, userId: currentUser.Id);
        
        Domain.Enitites.Category? category = await categoriesWriteRepository.GetCategoryByIdToUpdate(categoryId: request.CategoryId, userId: currentUser.Id);

        if (category == null)
        {
            throw new NotFoundException(message: ResourceErrorMessage.CATEGORY_NOT_FOUND);
        }

        Domain.Enitites.CategoryGoal categoryGoal = mapper.Map<Domain.Enitites.CategoryGoal>(source: request);
        categoryGoal.UserId = currentUser.Id;

        await writeRepository.Add(categoryGoal: categoryGoal);
        await unitOfWork.Commit();

        return mapper.Map<ResponseRegisterCategoryGoalJson>(source: categoryGoal);
    }

    private async Task Validate(RequestRegisterCategoryGoalJson request, Guid userId)
    {
        RegisterCategoryGoalValidator validator = new();
        ValidationResult result = validator.Validate(instance: request);

        Domain.Enitites.CategoryGoal? categoryGoalExist = await readRepository.GetCategoryGoalByCategoryId(categoryId: request.CategoryId, userId: userId);

        if (categoryGoalExist != null)
        {
           result.Errors.Add(item: new ValidationFailure(propertyName: string.Empty, errorMessage: ResourceErrorMessage.CATEGORY_GOAL_ALREADY_EXISTS));
        }
        
        if (!result.IsValid)
        {
            List<string> errors = result.Errors.Select(selector: error => error.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorsMessages: errors);
        }
    }
}
