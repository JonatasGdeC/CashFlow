using CashFlow.Application.AutoMapper;
using CashFlow.Application.UsesCases.Expense.Delete;
using CashFlow.Application.UsesCases.Expense.GetAll;
using CashFlow.Application.UsesCases.Expense.GetById;
using CashFlow.Application.UsesCases.Expense.GetDashboard;
using CashFlow.Application.UsesCases.Expense.Register;
using CashFlow.Application.UsesCases.Expense.Reports.Excel;
using CashFlow.Application.UsesCases.Expense.Reports.Pdf;
using CashFlow.Application.UsesCases.Expense.Update;
using CashFlow.Application.UsesCases.User.Delete;
using CashFlow.Application.UsesCases.User.Get;
using CashFlow.Application.UsesCases.User.Login;
using CashFlow.Application.UsesCases.User.Register;
using CashFlow.Application.UsesCases.User.Update;
using CashFlow.Application.UsesCases.User.UpdatePassword;
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
        services.AddScoped<IGetExpenseDashboard, GetExpenseDashboard>();
        
        services.AddScoped<IGenerateExpensesReportExcelUseCase, GenerateExpensesReportExcelUseCase>();
        services.AddScoped<IGenerateExpensesReportPdfUseCase, GenerateExpensesReportPdfUseCase>();
        
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<IGetUserProfileUseCase, GetUserProfileUseCase>();
        services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
        services.AddScoped<IChangePasswordUseCase, ChangePasswordUseCase>();
        services.AddScoped<IDeleteUserAccountUseCase, DeleteUserAccountUseCase>();
        
        services.AddScoped<ILoginUseCase, LoginUseCase>();
    }
}
