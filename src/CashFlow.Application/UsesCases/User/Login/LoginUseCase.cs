using CashFlow.Communication.Requests;
using CashFlow.Communication.Requests.User;
using CashFlow.Communication.Response;
using CashFlow.Communication.Response.User;
using CashFlow.Domain.Repositories.Users;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UsesCases.User.Login;

public class LoginUseCase(
    IUsersReadRepository readRepository,
    IPasswordEncrypter passwordEncrypter,
    IAccessTokenGenerator accessTokenGenerator) : ILoginUseCase
{
    public async Task<ResponseRegisterUserJson> Execute(RequestLoginJson request)
    {
        Domain.Enitites.User? user = await readRepository.GetUserByEmail(email: request.Email);
        if (user == null)
        {
            throw new InvalidLoginException();
        }

        bool passwordMatch = passwordEncrypter.Verify(password: request.Password, hash: user.Password);
        if (!passwordMatch)
        {
            throw new InvalidLoginException();
        }

        return new ResponseRegisterUserJson
        {
            Name = user.Name,
            Token = accessTokenGenerator.Generate(user: user)
        };
    }
}