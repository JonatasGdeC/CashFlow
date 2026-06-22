using CashFlow.Domain.Enitites;
using CashFlow.Domain.Security.Tokens;
using Moq;

namespace CommomTestsUtilies.Token;

public static class JwtTokenGeneratorBuilder
{
    public static IAccessTokenGenerator Build()
    {
        Mock<IAccessTokenGenerator> mock = new();
        mock.Setup(expression: config => config.Generate(It.IsAny<User>())).Returns(value: "token");
        return mock.Object;
    }
}