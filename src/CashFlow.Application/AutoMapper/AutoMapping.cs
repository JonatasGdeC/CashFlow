using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Response;
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
        
        CreateMap<RequestRegisterUserJson, User>().ForMember(destinationMember: dest => dest.Password, memberOptions: config => config.Ignore());
        CreateMap<RequestUpdateUserJson, User>();
    }
    
    private void EntityToResponse()
    {
        CreateMap<Expense, ResponseRegisterExpenseJson>();
        CreateMap<Expense, ResponseExpenseShortJson>();
        CreateMap<Expense, ResponseExpenseJson>();
        
        CreateMap<User, ResponseUserProfileJson>();
    }
}