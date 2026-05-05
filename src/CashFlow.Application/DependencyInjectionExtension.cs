using CashFlow.Application.AutoMapper;
using CashFlow.Application.UsesCases.Expense.Delete;
using CashFlow.Application.UsesCases.Expense.GetAll;
using CashFlow.Application.UsesCases.Expense.GetById;
using CashFlow.Application.UsesCases.Expense.Register;
using CashFlow.Application.UsesCases.Expense.Reports.Excel;
using CashFlow.Application.UsesCases.Expense.Update;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Application;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddAutoMapperApplication(services: services);
        AddUseCases(services: services);
    }

    private static void AddAutoMapperApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(configAction: config => { }, typeof(AutoMapping));
    }

    private static void AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<IRegisterExpenseUseCase, RegisterExpenseUseCase>();
        services.AddScoped<IGetAllExpenseUseCase, GetAllExpenseUseCase>();
        services.AddScoped<IGetByIdExpenseUseCase, GetByIdExpenseUseCase>();
        services.AddScoped<IDeleteExpenseUseCase, DeleteExpenseUseCase>();
        services.AddScoped<IUpdateExpenseUseCase, UpdateExpenseUseCase>();
        
        services.AddScoped<IGenerateExpensesReportExcelUseCase, GenerateExpensesReportExcelUseCase>();
    }
}
