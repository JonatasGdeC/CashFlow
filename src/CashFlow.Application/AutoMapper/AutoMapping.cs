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
    }
    
    private void EntityToResponse()
    {
        CreateMap<Expense,  ResponseRegisterExpenseJson>();
        CreateMap<Expense,  ResponseExpenseShortJson>();
    }
}