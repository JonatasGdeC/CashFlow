using CashFlow.Application.UsesCases.User.Login;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Requests.User;
using CashFlow.Communication.Response;
using CashFlow.Communication.Response.User;
using CashFlow.Domain.Repositories.Users;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using CommomTestsUtilies.Cryptography;
using CommomTestsUtilies.Entities;
using CommomTestsUtilies.Repositories;
using CommomTestsUtilies.Requests;
using CommomTestsUtilies.Token;
using FluentAssertions;
using FluentAssertions.Specialized;

namespace UseCases.Tests.User.Login;

public class LoginUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        CashFlow.Domain.Enitites.User user = UserBuilder.Build();
        RequestLoginJson request = RequestLoginJsonBuilder.Build();
        request.Email = user.Email;
        LoginUseCase useCase = CreateUseCase(user: user, password: request.Password);
        
        ResponseRegisterUserJson result = await useCase.Execute(request: request);

        result.Should().NotBeNull();
        result.Name.Should().Be(expected: user.Name);
        result.Token.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Error_User_Not_Found()
    {
        CashFlow.Domain.Enitites.User user = UserBuilder.Build();
        RequestLoginJson request = RequestLoginJsonBuilder.Build();
        LoginUseCase useCase = CreateUseCase(user: user, password: request.Password);

        Func<Task<ResponseRegisterUserJson>> act = async () => await useCase.Execute(request: request);

        ExceptionAssertions<InvalidLoginException>? result = await act.Should().ThrowAsync<InvalidLoginException>();
        result.Where(exceptionExpression: ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessage.EMAIL_OR_PASSWORD_INVALID));
    }

    [Fact]
    public async Task Error_Password_Not_Match()
    {
        CashFlow.Domain.Enitites.User user = UserBuilder.Build();
        RequestLoginJson request = RequestLoginJsonBuilder.Build();
        request.Email = user.Email;
        LoginUseCase useCase = CreateUseCase(user: user);

        Func<Task<ResponseRegisterUserJson>> act = async () => await useCase.Execute(request: request);

        ExceptionAssertions<InvalidLoginException>? result = await act.Should().ThrowAsync<InvalidLoginException>();
        result.Where(exceptionExpression: ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessage.EMAIL_OR_PASSWORD_INVALID));
    }
    
    private LoginUseCase CreateUseCase(CashFlow.Domain.Enitites.User user, string? password = null)
    {
        IUsersReadRepository readRepository = new UsersReadRepositoryBuilder().GetUserByEmail(user: user).Build();
        IAccessTokenGenerator accessTokenGenerator = JwtTokenGeneratorBuilder.Build();
        IPasswordEncrypter passwordEncrypter =  new PasswordEncrypterBuilder().Verify(password: password).Build();
        
        return new LoginUseCase(
            readRepository: readRepository, 
            passwordEncrypter: passwordEncrypter,
            accessTokenGenerator: accessTokenGenerator);
    }
}