using CashFlow.Communication.Requests;
using CashFlow.Domain.Enitites;
using CashFlow.Domain.Repositories.Expenses;
using Moq;

namespace CommomTestsUtilies.Repositories;

public class ExpensesReadRepositoryBuilder
{
    private readonly Mock<IExpensesReadRepository> _repository = new();

    public ExpensesReadRepositoryBuilder GetAll(User user, List<Expense> expenses)
    {
        _repository.Setup(expression: repository => repository.GetAllExpenses(userId: user.Id)).ReturnsAsync(value: expenses);
        return this;
    }

    public ExpensesReadRepositoryBuilder GetById(User user, Expense? expense)
    {
        if (expense is not null)
        {
            _repository.Setup(expression: repository => repository.GetExpenseById(expenseId: expense.Id, userId: user.Id)).ReturnsAsync(value: expense);
        }

        return this;
    }

    public ExpensesReadRepositoryBuilder FilterByMonth(User user, List<Expense> expenses)
    {
        RequestFilterJson request = new()
        {
            Date = It.IsAny<DateOnly>()
        };

        _repository.Setup(expression: repository => repository.GetFilter(request: request, userId: user.Id)).ReturnsAsync(value: expenses);

        return this;
    }

    public IExpensesReadRepository Build() => _repository.Object;
}