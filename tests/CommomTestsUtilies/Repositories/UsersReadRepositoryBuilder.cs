using CashFlow.Domain.Enitites;
using CashFlow.Domain.Repositories.Users;
using Moq;

namespace CommomTestsUtilies.Repositories;

public class UsersReadRepositoryBuilder
{
    private readonly Mock<IUsersReadRepository> _repositoryMock;
    
    public UsersReadRepositoryBuilder()
    {
        _repositoryMock = new Mock<IUsersReadRepository>();
    }

    public void ExistsUsersWithThisEmail(string email)
    {
        _repositoryMock.Setup(expression: repository => repository.ExistsUsersWithThisEmail(email)).ReturnsAsync(value: true);
    }

    public UsersReadRepositoryBuilder GetUserByEmail(User user)
    {
        _repositoryMock.Setup(expression: repository => repository.GetUserByEmail(user.Email)).ReturnsAsync(value: user);
        return this;
    }
    
    public IUsersReadRepository Build() => _repositoryMock.Object;
}