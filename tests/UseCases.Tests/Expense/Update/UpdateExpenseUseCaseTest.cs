using AutoMapper;
using CashFlow.Application.UsesCases.Expense.Update;
using CashFlow.Communication.Requests;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Categories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using CommomTestsUtilies.Entities;
using CommomTestsUtilies.LoggedUser;
using CommomTestsUtilies.Mapper;
using CommomTestsUtilies.Repositories;
using CommomTestsUtilies.Requests;
using FluentAssertions;
using FluentAssertions.Specialized;
using Moq;

namespace UseCases.Tests.Expense.Update;

public class UpdateExpenseUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        CashFlow.Domain.Enitites.User loggedUser = UserBuilder.Build();
        CashFlow.Domain.Enitites.Expense expense = ExpenseBuilder.Build(user: loggedUser);
        Guid expenseId = expense.Id;
        RequestRegisterExpenseJson request = RequestRegisterExpenseJsonBuilder.Build();

        UpdateExpenseUseCase useCase = CreateUseCase(user: loggedUser, expense: expense);

        Func<Task> act = async () => await useCase.Execute(id: expenseId, request: request);

        await act.Should().NotThrowAsync();
        expense.Id.Should().Be(expected: expenseId);
        expense.Title.Should().Be(expected: request.Title);
        expense.Description.Should().Be(expected: request.Description);
        expense.Date.Should().Be(expected: request.Date);
        expense.Amount.Should().Be(expected: request.Amount);
        expense.PaymentType.Should().Be(expected: (CashFlow.Domain.Enums.PaymentType)request.PaymentType);
    }

    [Fact]
    public async Task Error_Expense_Not_Found()
    {
        CashFlow.Domain.Enitites.User loggedUser = UserBuilder.Build();
        RequestRegisterExpenseJson request = RequestRegisterExpenseJsonBuilder.Build();
        UpdateExpenseUseCase useCase = CreateUseCase(user: loggedUser);

        Func<Task> act = async () => await useCase.Execute(id: Guid.NewGuid(), request: request);

        ExceptionAssertions<NotFoundException>? result = await act.Should().ThrowAsync<NotFoundException>();
        result.Where(exceptionExpression: ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessage.EXPENSE_NOT_FOUND));
    }

    [Fact]
    public async Task Error_Title_Empty()
    {
        CashFlow.Domain.Enitites.User loggedUser = UserBuilder.Build();
        CashFlow.Domain.Enitites.Expense expense = ExpenseBuilder.Build(user: loggedUser);
        RequestRegisterExpenseJson request = RequestRegisterExpenseJsonBuilder.Build();
        request.Title = String.Empty;
        UpdateExpenseUseCase useCase = CreateUseCase(user: loggedUser, expense: expense);

        Func<Task> act = async () => await useCase.Execute(id: expense.Id, request: request);

        ExceptionAssertions<ErrorOnValidationException>? result = await act.Should().ThrowAsync<ErrorOnValidationException>();
        result.Where(exceptionExpression: ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessage.EXPENSE_TITLE_REQUIRED));
    }

    private UpdateExpenseUseCase CreateUseCase(CashFlow.Domain.Enitites.User user, CashFlow.Domain.Enitites.Expense? expense = null)
    {
        IExpensesWriteRepository repository = new ExpensesWriteRepositoryBuilder().GetById(user: user, expense: expense).Build();
        ICategoriesReadRepository categoriesReadRepository = new Mock<ICategoriesReadRepository>().Object;
        IUnitOfWork unitOfWork = UnitOfWorkBuilder.Build();
        IMapper mapper = MapperBuilder.Build();
        ILoggedUser loggedUser = LoggedUserBuilder.Build(user: user);

        return new UpdateExpenseUseCase(
            writeRepository: repository,
            categoriesReadRepository: categoriesReadRepository,
            unitOfWork: unitOfWork,
            mapper: mapper,
            loggedUser: loggedUser);
    }
}
