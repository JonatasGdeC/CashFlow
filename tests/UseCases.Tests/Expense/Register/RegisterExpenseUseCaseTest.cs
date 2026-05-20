using AutoMapper;
using CashFlow.Application.UsesCases.Expense.Register;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Response;
using CashFlow.Domain.Repositories;
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

namespace UseCases.Tests.Expense.Register;

public class RegisterExpenseUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        CashFlow.Domain.Enitites.User loggedUser = UserBuilder.Build();
        RequestRegisterExpenseJson request = RequestRegisterExpenseJsonBuilder.Build();
        RegisterExpenseUseCase useCase = CreateUseCase(user: loggedUser);

        ResponseRegisterExpenseJson result = await useCase.Execute(request: request);

        result.Should().NotBeNull();
        result.Title.Should().NotBeNullOrWhiteSpace();
        result.Title.Should().Be(expected: request.Title);
    }
    
    [Fact]
    public async Task Error_Title_Empty()
    {
        CashFlow.Domain.Enitites.User loggedUser = UserBuilder.Build();
        RequestRegisterExpenseJson request = RequestRegisterExpenseJsonBuilder.Build();
        request.Title = String.Empty;
        RegisterExpenseUseCase useCase = CreateUseCase(user: loggedUser);
        
        Func<Task<ResponseRegisterExpenseJson>> act = async () => await useCase.Execute(request: request);

        ExceptionAssertions<ErrorOnValidationException>? result = await act.Should().ThrowAsync<ErrorOnValidationException>();
        result.Where(exceptionExpression: ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessage.EXPENSE_TITLE_REQUIRED));
    }
    
    private RegisterExpenseUseCase CreateUseCase(CashFlow.Domain.Enitites.User user)
    {
        IExpensesWriteRepository repository = ExpensesWriteRepositoryBuilder.Build();
        IMapper mapper = MapperBuilder.Build();
        IUnitOfWork unitOfWork = UnitOfWorkBuilder.Build();
        ILoggedUser loggedUser = LoggedUserBuilder.Build(user: user);

        return new RegisterExpenseUseCase(writeRepository: repository, unitOfWork: unitOfWork, mapper: mapper, loggedUser: loggedUser);
    }
}