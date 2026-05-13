using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Response;
using CashFlow.Domain.Repositories.Users;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using FluentValidation.Results;

namespace CashFlow.Application.UsesCases.User.Register;

public class RegisterUserUseCase(IPasswordEncrypter encrypter, IUsersReadRepository readRepository, IMapper mapper) : IRegisterUserUseCase
{
    public async Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson request)
    {
        await Validate(request: request);
        
        Domain.Enitites.User? user = mapper.Map<Domain.Enitites.User>(source: request);
        await readRepository.ExistsUsersWithThisEmail(email: user.Email);
        user.Password = encrypter.Encrypt(password: request.Password);
            
        return new ResponseRegisterUserJson
        {
            Name = user?.Name,
            Token = ""
        };

        throw new NotImplementedException();
    }
    
    private async Task Validate(RequestRegisterUserJson request)
    {
        ValidationResult resultRequest = new RegisterUserValidator().Validate(instance: request);
        bool emailExist = await readRepository.ExistsUsersWithThisEmail(email: request.Email);
        if (!emailExist)
        {
            resultRequest.Errors.Add(item: new ValidationFailure(propertyName: string.Empty, errorMessage: ResourceErrorMessage.USER_EMAIL_ALREADY_EXIST));
        }
        
        ValidationResult resultPassword = new PasswordValidator(password: request.Password).Validate(instance: request.Password);

        if (!resultRequest.IsValid || !resultPassword.IsValid)
        {
            List<string> errors = resultRequest.Errors.Concat(second: resultPassword.Errors).Select(selector: error => error.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorsMessages: errors);
        }
    }
}