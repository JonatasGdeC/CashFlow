using CashFlow.Communication.Requests;
using CashFlow.Communication.Requests.User;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Users;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using FluentValidation.Results;

namespace CashFlow.Application.UsesCases.User.UpdatePassword;

public class ChangePasswordUseCase(
    ILoggedUser loggedUser,
    IPasswordEncrypter encrypter,
    IUsersWriteRepository writeRepository,
    IUnitOfWork unitOfWork) : IChangePasswordUseCase
{
    public async Task Execute(RequestChangePasswordJson request)
    {
        Domain.Enitites.User currentUser = await loggedUser.Get();
        
        Validate(request: request, currentUser: currentUser);
        
        currentUser.Password = encrypter.Encrypt(password: request.NewPassword);
        writeRepository.Update(user: currentUser);
        await unitOfWork.Commit();
    }
    
    private void Validate(RequestChangePasswordJson request, Domain.Enitites.User currentUser)
    {
        ValidationResult resultPassword = new PasswordValidator().Validate(instance: request.NewPassword);

        if (!resultPassword.IsValid)
        {
            List<string> errors = resultPassword.Errors.Select(selector: error => error.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorsMessages: errors);
        }
        
        bool passwordMatch = encrypter.Verify(password: request.OldPassword, hash: currentUser.Password);
        if (!passwordMatch)
        {
            throw new BadRequestException(message: ResourceErrorMessage.OLD_PASSWORD_INVALID);
        }
    }
}