using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Infrastructure.DataAccess;
using CommomTestsUtilies.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Tests;
using CashFlow.Domain.Enitites;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    public User User { get; private set; } = null!;
    public string UserPassword { get; private set; } = null!;
    public string Token { get; private set; } = null!;
    public CashFlow.Domain.Enitites.Expense Expense { get; private set; } = null!;

    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment(environment: "Test")
            .ConfigureServices(configureServices: services =>
            {
                ServiceProvider provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                services.AddDbContext<CashFlowDbContext>(optionsAction: config =>
                {
                    config.UseInMemoryDatabase(databaseName: "InMemoryDbForTesting");
                    config.UseInternalServiceProvider(serviceProvider: provider);
                });

                IServiceScope scope = services.BuildServiceProvider().CreateScope();
                CashFlowDbContext dbContext = scope.ServiceProvider.GetRequiredService<CashFlowDbContext>();
                IPasswordEncrypter encrypter = scope.ServiceProvider.GetRequiredService<IPasswordEncrypter>();
                IAccessTokenGenerator tokenGenerator = scope.ServiceProvider.GetRequiredService<IAccessTokenGenerator>();

                StartDataBase(dbContext: dbContext, encrypter: encrypter);
                Token = tokenGenerator.Generate(user: User);
            });
    }

    private void StartDataBase(CashFlowDbContext dbContext, IPasswordEncrypter encrypter)
    {
        AddUser(dbContext: dbContext, encrypter: encrypter);
        AddExpense(dbContext: dbContext);
        dbContext.SaveChanges();
    }

    private void AddUser(CashFlowDbContext dbContext, IPasswordEncrypter encrypter)
    {
        User = UserBuilder.Build();
        UserPassword = User.Password;
        User.Password = encrypter.Encrypt(password: User.Password);
        dbContext.Users.Add(entity: User);
    }

    private void AddExpense(CashFlowDbContext dbContext)
    {
        Expense = ExpenseBuilder.Build(user: User);
        dbContext.Expenses.Add(entity: Expense);
    }
}