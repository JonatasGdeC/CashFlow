using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Repositories.Users;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Infrastructure.DataAccess;
using CashFlow.Infrastructure.DataAccess.Repositories;
using CashFlow.Infrastructure.Extensions;
using CashFlow.Infrastructure.Security.Tokens;
using CashFlow.Infrastructure.Services.LoggedUser;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration, string connectionString)
    {
        AddRepositories(services: services);
        AddToken(services: services, configuration: configuration);
        
        services.AddScoped<IPasswordEncrypter, Security.Cryptography.BCrypt>();
        services.AddScoped<ILoggedUser, LoggedUser>();
        
        if (!configuration.IsTestEnvironment())
        {
            AddDbContext(services: services, connectionString: connectionString);
        }
    }

    private static void AddToken(IServiceCollection services, IConfiguration configuration)
    {
        uint expirationTimeMinutes = configuration.GetValue<uint>(key: "Settings:Jwt:ExpiresMinutes");
        string signingKey = configuration.GetValue<string>(key: "Settings:Jwt:SigningKey")!;

        services.AddScoped<IAccessTokenGenerator>(implementationFactory: config => new JwtTokenGenerator(expirationTimeMinutes: expirationTimeMinutes, signingKey: signingKey));
    }

    private static void AddDbContext(IServiceCollection services, string connectionString)
    { 
        ServerVersion? serverVersion = ServerVersion.AutoDetect(connectionString: connectionString);
        services.AddDbContext<CashFlowDbContext>(optionsAction: config => config.UseMySql(connectionString: connectionString, serverVersion: serverVersion));
    }
    
    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.AddScoped<IExpensesWriteRepository, ExpensesesRepository>();
        services.AddScoped<IExpensesReadRepository, ExpensesesRepository>();
        
        services.AddScoped<IUsersReadRepository, UserRepository>();
        services.AddScoped<IUsersWriteRepository, UserRepository>();
    }
}