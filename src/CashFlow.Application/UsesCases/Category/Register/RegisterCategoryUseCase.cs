using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Requests.Category;
using CashFlow.Communication.Response;
using CashFlow.Communication.Response.Category;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Categories;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using FluentValidation.Results;

namespace CashFlow.Application.UsesCases.Category.Register;

public class RegisterCategoryUseCase(
    ICategoriesWriteRepository writeRepository,
    ICategoriesReadRepository readRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ILoggedUser loggedUser) : IRegisterCategoryUseCase
{
    public async Task<ResponseRegisterCategoryJson> Execute(RequestRegisterCategoryJson request)
    {
        Domain.Enitites.User currentUser = await loggedUser.Get();
        await Validate(request: request, userId: currentUser.Id);
        
        Domain.Enitites.Category category = mapper.Map<Domain.Enitites.Category>(source: request);
        category.UserId = currentUser.Id;

        await writeRepository.Add(category: category);
        await unitOfWork.Commit();

        return mapper.Map<ResponseRegisterCategoryJson>(source: category);
    }

    private async Task Validate(RequestRegisterCategoryJson request, Guid userId)
    {
        RegisterCategoryValidator validator = new();
        ValidationResult result = validator.Validate(instance: request);

        Domain.Enitites.Category? categoryExist = await readRepository.GetCategoryByTitle(title: request.Name, userId: userId);
        if (categoryExist != null)
        {
            result.Errors.Add(item: new ValidationFailure(propertyName: string.Empty, errorMessage: ResourceErrorMessage.CATEGORY_ALREADY_EXISTS));
        }
        
        if (!result.IsValid)
        {
            List<string> errors = result.Errors.Select(selector: error => error.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorsMessages: errors);
        }
    }
}