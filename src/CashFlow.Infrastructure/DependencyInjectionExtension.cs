using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Infrastructure.DataAccess;
using CashFlow.Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Infrastructure;

public static class DependencyInjectionExtension
{
  public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
  {
    AddDbContext(services: services, configuration: configuration);
    AddRepositories(services: services);
  }

  private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
  {
    string? connectionString = configuration.GetConnectionString(name: "connection");;

    Version version = new Version(major: 8, minor: 0, build: 41);
    MySqlServerVersion serverVersion = new MySqlServerVersion(version: version);

    services.AddDbContext<CashFlowDbContext>(optionsAction: config => config.UseMySql(connectionString: connectionString, serverVersion: serverVersion));
  }

  private static void AddRepositories(IServiceCollection services)
  {
    services.AddScoped<IExpensesReadOnlyRepository, ExpensesRepository>();
    services.AddScoped<IExpensesWhiteOnlyRepository, ExpensesRepository>();
    services.AddScoped<IExpensesUpdateOnlyRepository, ExpensesRepository>();
    services.AddScoped<IUnitOfWork, UnitOfWork>();
  }

}
