using AutoMapper;
using CashFlow.Application.UsesCases.Expense.GetById;
using CashFlow.Communication.Response;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using CommomTestsUtilies.Entities;
using CommomTestsUtilies.LoggedUser;
using CommomTestsUtilies.Mapper;
using CommomTestsUtilies.Repositories;
using FluentAssertions;
using FluentAssertions.Specialized;

namespace UseCases.Tests.Expense.GetById;

public class GetExpenseByIdUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        CashFlow.Domain.Enitites.User loggedUser = UserBuilder.Build();
        CashFlow.Domain.Enitites.Expense expense = ExpenseBuilder.Build(user: loggedUser);

        GetByIdExpenseUseCase useCase = CreateUseCase(user: loggedUser, expense: expense);

        ResponseExpenseJson result = await useCase.Execute(id: expense.Id);

        result.Should().NotBeNull();
        result.Id.Should().Be(expected: expense.Id);
        result.Title.Should().Be(expected: expense.Title);
        result.Description.Should().Be(expected: expense.Description);
        result.Date.Should().Be(expected: expense.Date);
        result.Amount.Should().Be(expected: expense.Amount);
        result.PaymentType.Should().Be(expected: (CashFlow.Communication.Enums.PaymentType)expense.PaymentType);
    }

    [Fact]
    public async Task Error_Expense_Not_Found()
    {
        CashFlow.Domain.Enitites.User loggedUser = UserBuilder.Build();
        GetByIdExpenseUseCase useCase = CreateUseCase(user: loggedUser);
    
        Func<Task<ResponseExpenseJson>> act = async () => await useCase.Execute(id: Guid.NewGuid());
    
        ExceptionAssertions<NotFoundException>? result = await act.Should().ThrowAsync<NotFoundException>();
        result.Where(exceptionExpression: ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessage.EXPENSE_NOT_FOUND));
    }

    private GetByIdExpenseUseCase CreateUseCase(CashFlow.Domain.Enitites.User user, CashFlow.Domain.Enitites.Expense? expense = null)
    {
        IExpensesReadRepository repository = new ExpensesReadRepositoryBuilder().GetById(user: user, expense: expense).Build();
        IMapper mapper = MapperBuilder.Build();
        ILoggedUser loggedUser = LoggedUserBuilder.Build(user: user);

        return new GetByIdExpenseUseCase(readRepository: repository, mapper: mapper, loggedUser: loggedUser);
    }
}