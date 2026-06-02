using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Requests.User;
using CashFlow.Communication.Response;
using CashFlow.Communication.Response.User;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Users;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using FluentValidation.Results;

namespace CashFlow.Application.UsesCases.User.Register;

public class RegisterUserUseCase(
    IPasswordEncrypter encrypter,
    IUsersReadRepository readRepository,
    IUsersWriteRepository writeRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IAccessTokenGenerator tokenGenerator) : IRegisterUserUseCase
{
    public async Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson request)
    {
        await Validate(request: request);

        Domain.Enitites.User? user = mapper.Map<Domain.Enitites.User>(source: request);
        await readRepository.ExistsUsersWithThisEmail(email: user.Email);
        user.Password = encrypter.Encrypt(password: request.Password);

        await writeRepository.Add(user: user);
        await unitOfWork.Commit();

        return new ResponseRegisterUserJson
        {
            Name = user.Name,
            Token = tokenGenerator.Generate(user: user)
        };
    }

    private async Task Validate(RequestRegisterUserJson request)
    {
        ValidationResult resultRequest = new RegisterUserValidator().Validate(instance: request);
        bool emailExist = await readRepository.ExistsUsersWithThisEmail(email: request.Email);
        if (emailExist)
        {
            resultRequest.Errors.Add(item: new ValidationFailure(propertyName: string.Empty, errorMessage: ResourceErrorMessage.USER_EMAIL_ALREADY_EXIST));
        }

        ValidationResult resultPassword = new PasswordValidator().Validate(instance: request.Password);

        if (!resultRequest.IsValid || !resultPassword.IsValid)
        {
            List<string> errors = resultRequest.Errors.Concat(second: resultPassword.Errors).Select(selector: error => error.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorsMessages: errors);
        }
    }
}