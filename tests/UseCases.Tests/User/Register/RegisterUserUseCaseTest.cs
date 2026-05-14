using AutoMapper;
using CashFlow.Application.UsesCases.User.Register;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Response;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Users;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Security.Tokens;
using CommomTestsUtilies.Cryptography;
using CommomTestsUtilies.Mapper;
using CommomTestsUtilies.Repositories;
using CommomTestsUtilies.Requests;
using CommomTestsUtilies.Token;
using FluentAssertions;

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

    private RegisterUserUseCase CreateUseCase()
    {
        IMapper mapper = MapperBuilder.Build();
        IUnitOfWork unitOfWork = UnitOfWorkBuilder.Build();
        IUsersWriteRepository writeRepository = UsersWriteRepositoryBuilder.Build();
        IAccessTokenGenerator accessTokenGenerator = JwtTokenGeneratorBuilder.Build();
        IPasswordEncrypter passwordEncrypter = PasswordEncrypterBuilder.Build();
        IUsersReadRepository readRepository = new UsersReadRepositoryBuilder().Build();

        return new RegisterUserUseCase(
            mapper: mapper, 
            unitOfWork: unitOfWork, 
            writeRepository: writeRepository, 
            readRepository: readRepository,
            tokenGenerator: accessTokenGenerator, 
            encrypter: passwordEncrypter);
    }
}