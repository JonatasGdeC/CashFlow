using AutoMapper;
using CashFlow.Application.UsesCases.Expense.GetAll;
using CashFlow.Communication.Response;
using CashFlow.Communication.Response.Expense;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Services.LoggedUser;
using CommomTestsUtilies.Entities;
using CommomTestsUtilies.LoggedUser;
using CommomTestsUtilies.Mapper;
using CommomTestsUtilies.Repositories;
using FluentAssertions;

namespace UseCases.Tests.Expense.GetAll;

public class GetAllExpenseUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        CashFlow.Domain.Enitites.User loggedUser = UserBuilder.Build();
        List<CashFlow.Domain.Enitites.Expense> expenses = ExpenseBuilder.Collection(user: loggedUser);

        GetAllExpenseUseCase useCase = CreateUseCase(user: loggedUser, expenses: expenses);

        ResponseGetAllExpensesJson result = await useCase.Execute();

        result.Should().NotBeNull();
        result.ListAllExpenses.Should().NotBeNullOrEmpty().And.AllSatisfy(expected: expense =>
        {
            expense.Title.Should().NotBeNullOrEmpty();
            expense.Amount.Should().BeGreaterThan(expected: 0);
        });
    }

    private GetAllExpenseUseCase CreateUseCase(CashFlow.Domain.Enitites.User user, List<CashFlow.Domain.Enitites.Expense> expenses)
    {
        IExpensesReadRepository repository = new ExpensesReadRepositoryBuilder().GetAll(user: user, expenses: expenses).Build();
        IMapper mapper = MapperBuilder.Build();
        ILoggedUser loggedUser = LoggedUserBuilder.Build(user: user);

        return new GetAllExpenseUseCase(readRepository: repository, mapper: mapper, loggedUser: loggedUser);
    }
}