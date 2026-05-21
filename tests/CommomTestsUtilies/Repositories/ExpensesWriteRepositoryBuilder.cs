using CashFlow.Domain.Enitites;
using CashFlow.Domain.Repositories.Expenses;
using Moq;

namespace CommomTestsUtilies.Repositories;

public class ExpensesWriteRepositoryBuilder
{
    private readonly Mock<IExpensesWriteRepository> _repository = new();

    public ExpensesWriteRepositoryBuilder GetById(User user, Expense? expense)
    {
        if (expense is not null)
        {
            _repository.Setup(expression: repository => repository.GetExpenseByIdToUpdate(expenseId: expense.Id, userId: user.Id)).ReturnsAsync(value: expense);
        }

        return this;
    }

    public IExpensesWriteRepository Build()
    {
        return _repository.Object;
    }
}
