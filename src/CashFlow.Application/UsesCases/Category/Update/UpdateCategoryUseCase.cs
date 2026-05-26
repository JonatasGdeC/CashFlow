using AutoMapper;
using CashFlow.Application.UsesCases.Category.Register;
using CashFlow.Communication.Requests;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Categories;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using FluentValidation.Results;

namespace CashFlow.Application.UsesCases.Category.Update;

public class UpdateCategoryUseCase(
    ICategoriesWriteRepository writeRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ILoggedUser loggedUser) : IUpdateCategoryUseCase
{
    public async Task Execute(Guid id, RequestRegisterCategoryJson request)
    {
        Validate(request: request);

        Domain.Enitites.User currentUser = await loggedUser.Get();
        Domain.Enitites.Category? category = await writeRepository.GetCategoryByIdToUpdate(categoryId: id, userId: currentUser.Id);

        if (category == null)
        {
            throw new NotFoundException(message: ResourceErrorMessage.CATEGORY_NOT_FOUND);
        }

        mapper.Map(source: request, destination: category);
        category.Id = id;

        writeRepository.Update(category: category);
        await unitOfWork.Commit();
    }

    private void Validate(RequestRegisterCategoryJson request)
    {
        RegisterCategoryValidator validator = new();
        ValidationResult result = validator.Validate(instance: request);

        if (!result.IsValid)
        {
            List<string> errors = result.Errors.Select(selector: error => error.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorsMessages: errors);
        }
    }
}
