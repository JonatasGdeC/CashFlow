using AutoMapper;
using CashFlow.Application.UsesCases.Expense.Register;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Response;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Repositories.Users;
using CashFlow.Domain.Services.LoggedUser;
using CommomTestsUtilies.Entities;
using CommomTestsUtilies.LoggedUser;
using CommomTestsUtilies.Mapper;
using CommomTestsUtilies.Repositories;
using CommomTestsUtilies.Requests;
using FluentAssertions;

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
    
    private RegisterExpenseUseCase CreateUseCase(CashFlow.Domain.Enitites.User user)
    {
        IExpensesWriteRepository repository = ExpensesWriteRepositoryBuilder.Build();
        IMapper mapper = MapperBuilder.Build();
        IUnitOfWork unitOfWork = UnitOfWorkBuilder.Build();
        ILoggedUser loggedUser = LoggedUserBuilder.Build(user: user);

        return new RegisterExpenseUseCase(writeRepository: repository, unitOfWork: unitOfWork, mapper: mapper, loggedUser: loggedUser);
    }
}