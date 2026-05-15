using CashFlow.Domain.Security.Cryptography;
using Moq;

namespace CommomTestsUtilies.Cryptography;

public  class PasswordEncrypterBuilder
{
    private readonly Mock<IPasswordEncrypter> _mock;

    public PasswordEncrypterBuilder()
    {
        _mock = new Mock<IPasswordEncrypter>();

        _mock.Setup(expression: passwordEncrypter => passwordEncrypter.Encrypt(It.IsAny<string>())).Returns(value: "!%dlfjkd545");
    }

    public PasswordEncrypterBuilder Verify(string? password)
    {
        if(string.IsNullOrWhiteSpace(value: password) == false)
        {
            _mock.Setup(expression: passwordEncrypter => passwordEncrypter.Verify(password, It.IsAny<string>())).Returns(value: true);
        }
        
        return this;
    }

    public IPasswordEncrypter Build() => _mock.Object;
}