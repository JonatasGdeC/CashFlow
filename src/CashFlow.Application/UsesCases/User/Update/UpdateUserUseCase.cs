using AutoMapper;
using CashFlow.Application.UsesCases.User.Register;
using CashFlow.Communication.Requests;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Users;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using FluentValidation.Results;

namespace CashFlow.Application.UsesCases.User.Update;

public class UpdateUserUseCase(
    ILoggedUser loggedUser,
    IMapper mapper,
    IUsersWriteRepository writeRepository,
    IUsersReadRepository readRepository,
    IUnitOfWork unitOfWork) : IUpdateUserUseCase
{
    public async Task Execute(RequestUpdateUserJson request)
    {
        Domain.Enitites.User user = await loggedUser.Get();
        await Validate(request: request, currentEmail: user.Email);
        
        mapper.Map(source: request, destination: user);

        writeRepository.Update(user: user);
        await unitOfWork.Commit();
    }

    private async Task Validate(RequestUpdateUserJson request, string currentEmail)
    {
        ValidationResult resultRequest = new RegisterUserValidator().Validate(instance: request);
        
        if (!currentEmail.Equals(value: request.Email))
        {
            Domain.Enitites.User? userExist = await readRepository.GetUserByEmail(email: request.Email);
            if (userExist != null)
            {
                resultRequest.Errors.Add(item: new ValidationFailure(propertyName: string.Empty, errorMessage: ResourceErrorMessage.USER_EMAIL_ALREADY_EXIST));
            }
        }

        if (!resultRequest.IsValid)
        {
            List<string> errors = resultRequest.Errors.Select(selector: error => error.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorsMessages: errors);
        }
    }
}