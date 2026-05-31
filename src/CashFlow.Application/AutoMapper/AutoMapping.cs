using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Requests.Category;
using CashFlow.Communication.Requests.CategoryGoal;
using CashFlow.Communication.Requests.Expense;
using CashFlow.Communication.Requests.Income;
using CashFlow.Communication.Requests.User;
using CashFlow.Communication.Response;
using CashFlow.Communication.Response.Category;
using CashFlow.Communication.Response.CategoryGoal;
using CashFlow.Communication.Response.Expense;
using CashFlow.Communication.Response.Income;
using CashFlow.Communication.Response.User;
using CashFlow.Domain.Enitites;

namespace CashFlow.Application.AutoMapper;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToEntity();
        EntityToResponse();
    }

    private void RequestToEntity()
    {
        CreateMap<RequestRegisterExpenseJson, Expense>();
        CreateMap<RequestRegisterIncomeJson, Income>();
        CreateMap<RequestRegisterCategoryJson, Category>();
        CreateMap<RequestRegisterCategoryGoalJson, CategoryGoal>();
        
        CreateMap<RequestRegisterUserJson, User>().ForMember(destinationMember: dest => dest.Password, memberOptions: config => config.Ignore());
        CreateMap<RequestUpdateUserJson, User>();
    }
    
    private void EntityToResponse()
    {
        CreateMap<Expense, ResponseRegisterExpenseJson>();
        CreateMap<Expense, ResponseExpenseShortJson>();
        CreateMap<Expense, ResponseExpenseJson>();
        CreateMap<Income, ResponseRegisterIncomeJson>();
        CreateMap<Income, ResponseIncomeShortJson>();
        CreateMap<Income, ResponseIncomeJson>();
        CreateMap<Category, ResponseRegisterCategoryJson>();
        CreateMap<Category, ResponseCategoryShortJson>();
        CreateMap<Category, ResponseCategoryJson>();
        CreateMap<CategoryGoal, ResponseRegisterCategoryGoalJson>();
        CreateMap<CategoryGoal, ResponseCategoryGoalJson>();
        
        CreateMap<User, ResponseUserProfileJson>();
    }
}
