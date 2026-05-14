using CashFlow.Domain.Security.Cryptography;
using Moq;

namespace CommomTestsUtilies.Cryptography;

public class PasswordEncrypterBuilder
{
    public static IPasswordEncrypter Build()
    {
        Mock<IPasswordEncrypter> mock = new();
        mock.Setup(expression: config => config.Encrypt(It.IsAny<string>())).Returns(value: "encrypted");
        
        return mock.Object;
    }
}