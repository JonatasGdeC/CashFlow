using CashFlow.Application.AutoMapper;
using CashFlow.Application.UseCases.Expenses.Delete;
using CashFlow.Application.UseCases.Expenses.GetAll;
using CashFlow.Application.UseCases.Expenses.GetById;
using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Application.UseCases.Expenses.Update;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Application;

public static class DependencyInjectionExtension
{
  public static void AddApplication(this IServiceCollection services)
  {
    AddAutoMapper(services);
    AddUseCase(services);
  }

  private static void AddAutoMapper(this IServiceCollection services)
  {
    services.AddAutoMapper(typeof(AutoMapping));
  }

  private static void AddUseCase(this IServiceCollection services)
  {
    services.AddScoped<IRegisterExpenseUseCase, RegisterExpenseUseCase>();
    services.AddScoped<IGetAllExpenseUseCase, GetAllExpenseUseCase>();
    services.AddScoped<IGetExpenseByIdUseCase, GetExpenseByIdUseCase>();
    services.AddScoped<IDeleteExpenseUseCase, DeleteExpenseUseCase>();
    services.AddScoped<IUpdateExpenseUseCase, UpdateExpenseUseCase>();
  }
}
