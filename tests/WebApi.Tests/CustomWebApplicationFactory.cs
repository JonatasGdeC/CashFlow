using CashFlow.Domain.Enitites;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Infrastructure.DataAccess;
using CommomTestsUtilies.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Tests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    public User User { get; private set; } = default!;
    public string UserPassword { get; private set; } = default!;
    public string Token { get; private set; } = default!;

    
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
        User = UserBuilder.Build();
        UserPassword = User.Password;
        User.Password = encrypter.Encrypt(password: User.Password);
        dbContext.Users.Add(entity: User);
        dbContext.SaveChanges();
    }
}