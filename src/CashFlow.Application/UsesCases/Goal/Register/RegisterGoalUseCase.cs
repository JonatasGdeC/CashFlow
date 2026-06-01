using AutoMapper;
using CashFlow.Communication.Requests.Goal;
using CashFlow.Communication.Response.Goal;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Categories;
using CashFlow.Domain.Repositories.CategoriesGoals;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using FluentValidation.Results;

namespace CashFlow.Application.UsesCases.Goal.Register;

public class RegisterGoalUseCase(
    ICategoriesGoalsWriteRepository writeRepository,
    ICategoriesGoalsReadRepository readRepository,
    ICategoriesWriteRepository categoriesWriteRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ILoggedUser loggedUser) : IRegisterGoalUseCase
{
    public async Task<ResponseRegisterGoalJson> Execute(RequestRegisterGoalJson request)
    {
        Domain.Enitites.User currentUser = await loggedUser.Get();
        await Validate(request: request, userId: currentUser.Id);
        
        Domain.Enitites.Category? category = await categoriesWriteRepository.GetCategoryByIdToUpdate(categoryId: request.CategoryId, userId: currentUser.Id);

        if (category == null)
        {
            throw new NotFoundException(message: ResourceErrorMessage.CATEGORY_NOT_FOUND);
        }

        Domain.Enitites.Goal goal = mapper.Map<Domain.Enitites.Goal>(source: request);
        goal.UserId = currentUser.Id;

        await writeRepository.Add(goal: goal);
        await unitOfWork.Commit();

        return mapper.Map<ResponseRegisterGoalJson>(source: goal);
    }

    private async Task Validate(RequestRegisterGoalJson request, Guid userId)
    {
        RegisterGoalValidator validator = new();
        ValidationResult result = validator.Validate(instance: request);

        Domain.Enitites.Goal? categoryGoalExist = await readRepository.GetCategoryGoalByCategoryId(categoryId: request.CategoryId, userId: userId);

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
