using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Response;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Exception.ExceptionBase;
using FluentValidation.Results;

namespace CashFlow.Application.UsesCases.User.Register;

public class RegisterUserUseCase(IPasswordEncrypter encrypter, IMapper mapper) : IRegisterUserUseCase
{
    public async Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson request)
    {
        Validate(request: request);
        
        Domain.Enitites.User? user = mapper.Map<Domain.Enitites.User>(source: request);
        user.Password = encrypter.Encrypt(password: request.Password);
            
        return new ResponseRegisterUserJson
        {
            Name = user?.Name,
            Token = ""
        };

        throw new NotImplementedException();
    }
    
    private void Validate(RequestRegisterUserJson request)
    {
        ValidationResult resultRequest = new RegisterUserValidator().Validate(instance: request);
        ValidationResult resultPassword = new PasswordValidator(password: request.Password).Validate(instance: request.Password);

        if (!resultRequest.IsValid || !resultPassword.IsValid)
        {
            List<string> errors = resultRequest.Errors.Concat(second: resultPassword.Errors).Select(selector: error => error.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorsMessages: errors);
        }
    }
}