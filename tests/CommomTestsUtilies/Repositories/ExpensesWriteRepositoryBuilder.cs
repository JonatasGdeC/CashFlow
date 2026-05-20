using CashFlow.Domain.Repositories.Expenses;
using Moq;

namespace CommomTestsUtilies.Repositories;

public class ExpensesWriteRepositoryBuilder
{
    public static IExpensesWriteRepository Build()
    {
        Mock<IExpensesWriteRepository> mock = new();
        return mock.Object;
    }
}