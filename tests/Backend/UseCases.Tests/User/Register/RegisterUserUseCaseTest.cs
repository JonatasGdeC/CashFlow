using AutoMapper;
using CashFlow.Application.UsesCases.User.Register;
using CashFlow.Communication.Requests.User;
using CashFlow.Communication.Response.User;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Users;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using CommomTestsUtilies.Cryptography;
using CommomTestsUtilies.Mapper;
using CommomTestsUtilies.Repositories;
using CommomTestsUtilies.Requests;
using CommomTestsUtilies.Token;
using FluentAssertions;
using FluentAssertions.Specialized;

namespace UseCases.Tests.User.Register;

public class RegisterUserUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        RequestRegisterUserJson request = RequestRegisterUserJsonBuilder.Build();
        RegisterUserUseCase useCase = CreateUseCase();

        ResponseRegisterUserJson result = await useCase.Execute(request: request);

        result.Should().NotBeNull();
        result.Name.Should().Be(expected: request.Name);
        result.Token.Should().NotBeNullOrWhiteSpace();
    }
    
    [Fact]
    public async Task Error_Name_Empty()
    {
        RequestRegisterUserJson request = RequestRegisterUserJsonBuilder.Build();
        request.Name = string.Empty;
        RegisterUserUseCase useCase = CreateUseCase();
        
        Func<Task<ResponseRegisterUserJson>> act = async () => await useCase.Execute(request: request);

        ExceptionAssertions<ErrorOnValidationException>? result = await act.Should().ThrowAsync<ErrorOnValidationException>();
        result.Where(exceptionExpression: ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessage.USER_NAME_REQUIRED));
    }
    
    [Fact]
    public async Task Error_Email_Already_Exists()
    {
        RequestRegisterUserJson request = RequestRegisterUserJsonBuilder.Build();
        RegisterUserUseCase useCase = CreateUseCase(email: request.Email);
        
        Func<Task<ResponseRegisterUserJson>> act = async () => await useCase.Execute(request: request);

        ExceptionAssertions<ErrorOnValidationException>? result = await act.Should().ThrowAsync<ErrorOnValidationException>();
        result.Where(exceptionExpression: ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessage.USER_EMAIL_ALREADY_EXIST));
    }

    private RegisterUserUseCase CreateUseCase(string? email = null)
    {
        PasswordEncrypterBuilder passwordEncrypterBuilder = new();
        
        IMapper mapper = MapperBuilder.Build();
        IUnitOfWork unitOfWork = UnitOfWorkBuilder.Build();
        IUsersWriteRepository writeRepository = UsersWriteRepositoryBuilder.Build();
        IAccessTokenGenerator accessTokenGenerator = JwtTokenGeneratorBuilder.Build();
        IPasswordEncrypter passwordEncrypter = passwordEncrypterBuilder.Build();
        UsersReadRepositoryBuilder readRepository = new();

        if (!string.IsNullOrEmpty(value: email))
        {
            readRepository.ExistsUsersWithThisEmail(email: email);
        }

        return new RegisterUserUseCase(
            mapper: mapper, 
            unitOfWork: unitOfWork, 
            writeRepository: writeRepository, 
            readRepository: readRepository.Build(),
            tokenGenerator: accessTokenGenerator, 
            encrypter: passwordEncrypter);
    }
}