using CashFlow.Domain.Repositories.Users;
using Moq;

namespace CommomTestsUtilies.Repositories;

public class UsersWriteRepositoryBuilder
{
    public static IUsersWriteRepository Build()
    {
        Mock<IUsersWriteRepository> mock = new();
        return mock.Object;
    }
}