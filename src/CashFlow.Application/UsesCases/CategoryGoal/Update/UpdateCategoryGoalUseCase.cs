using AutoMapper;
using CashFlow.Application.UsesCases.CategoryGoal.Register;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Requests.CategoryGoal;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Categories;
using CashFlow.Domain.Repositories.CategoriesGoals;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using FluentValidation.Results;

namespace CashFlow.Application.UsesCases.CategoryGoal.Update;

public class UpdateCategoryGoalUseCase(
    ICategoriesGoalsWriteRepository writeRepository,
    ICategoriesWriteRepository categoriesWriteRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ILoggedUser loggedUser) : IUpdateCategoryGoalUseCase
{
    public async Task Execute(Guid id, RequestRegisterCategoryGoalJson request)
    {
        Validate(request: request);

        Domain.Enitites.User currentUser = await loggedUser.Get();
        Domain.Enitites.CategoryGoal? categoryGoal = await writeRepository.GetCategoryGoalByIdToUpdate(categoryGoalId: id, userId: currentUser.Id);

        if (categoryGoal == null)
        {
            throw new NotFoundException(message: ResourceErrorMessage.CATEGORY_GOAL_NOT_FOUND);
        }

        Domain.Enitites.Category? category = await categoriesWriteRepository.GetCategoryByIdToUpdate(categoryId: request.CategoryId, userId: currentUser.Id);

        if (category == null)
        {
            throw new NotFoundException(message: ResourceErrorMessage.CATEGORY_NOT_FOUND);
        }

        mapper.Map(source: request, destination: categoryGoal);
        categoryGoal.Id = id;

        writeRepository.Update(categoryGoal: categoryGoal);
        await unitOfWork.Commit();
    }

    private void Validate(RequestRegisterCategoryGoalJson request)
    {
        RegisterCategoryGoalValidator validator = new();
        ValidationResult result = validator.Validate(instance: request);

        if (!result.IsValid)
        {
            List<string> errors = result.Errors.Select(selector: error => error.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorsMessages: errors);
        }
    }
}
