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
    
    public IUsersReadRepository Build() => _repositoryMock.Object;
}