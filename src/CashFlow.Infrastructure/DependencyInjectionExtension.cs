using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Infrastructure.DataAccess;
using CashFlow.Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        AddDbContext(services: services, connectionString: connectionString);
        AddRepositories(services: services);
    }

    private static void AddDbContext(IServiceCollection services, string connectionString)
    { 
        MySqlServerVersion serverVersion = new(version: new Version(major: 8,minor: 0,build: 46));
        services.AddDbContext<CashFlowDbContext>(optionsAction: config => config.UseMySql(connectionString: connectionString, serverVersion: serverVersion));
    }
    
    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IExpensesRepository, ExpensesRepository>();
    }
}