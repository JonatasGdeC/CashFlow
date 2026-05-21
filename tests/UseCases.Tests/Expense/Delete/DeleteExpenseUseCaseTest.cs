using CashFlow.Application.UsesCases.Expense.Delete;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using CommomTestsUtilies.Entities;
using CommomTestsUtilies.LoggedUser;
using CommomTestsUtilies.Repositories;
using FluentAssertions;
using FluentAssertions.Specialized;

namespace UseCases.Tests.Expense.Delete;

public class DeleteExpenseUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        CashFlow.Domain.Enitites.User loggedUser = UserBuilder.Build();
        CashFlow.Domain.Enitites.Expense expense = ExpenseBuilder.Build(user: loggedUser);

        DeleteExpenseUseCase useCase = CreateUseCase(user: loggedUser, expense: expense);

        Func<Task> act = async () => await useCase.Execute(id: expense.Id);

        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task Error_Expense_Not_Found()
    {
        CashFlow.Domain.Enitites.User loggedUser = UserBuilder.Build();
        DeleteExpenseUseCase useCase = CreateUseCase(user: loggedUser);

        Func<Task> act = async () => await useCase.Execute(id: Guid.NewGuid());

        ExceptionAssertions<NotFoundException>? result = await act.Should().ThrowAsync<NotFoundException>();
        result.Where(exceptionExpression: ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessage.EXPENSE_NOT_FOUND));
    }

    private DeleteExpenseUseCase CreateUseCase(CashFlow.Domain.Enitites.User user, CashFlow.Domain.Enitites.Expense? expense = null)
    {
        IExpensesWriteRepository repository = new ExpensesWriteRepositoryBuilder().GetById(user: user, expense: expense).Build();
        IUnitOfWork unitOfWork = UnitOfWorkBuilder.Build();
        ILoggedUser loggedUser = LoggedUserBuilder.Build(user: user);

        return new DeleteExpenseUseCase(writeRepository: repository, unitOfWork: unitOfWork, loggedUser: loggedUser);
    }
}
