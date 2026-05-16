using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Tests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
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
                IPasswordEncrypter passwordEncrypter = scope.ServiceProvider.GetRequiredService<IPasswordEncrypter>();
                IAccessTokenGenerator accessTokenGenerator = scope.ServiceProvider.GetRequiredService<IAccessTokenGenerator>();

                // StartDatabase(dbContext, passwordEncrypter, accessTokenGenerator);
            });
    }
}