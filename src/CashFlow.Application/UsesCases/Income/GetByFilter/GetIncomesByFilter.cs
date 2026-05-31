using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Response.Income;
using CashFlow.Domain.Repositories.Incomes;
using CashFlow.Domain.Services.LoggedUser;

namespace CashFlow.Application.UsesCases.Income.GetByFilter;

public class GetIncomesByFilter(IIncomesReadRepository readRepository, ILoggedUser loggedUser, IMapper mapper) : IGetIncomesByFilter
{
    public async Task<ResponseGetAllIncomesJson> Execute(RequestFilterJson request)
    {
        Domain.Enitites.User currentUser = await loggedUser.Get();
        List<Domain.Enitites.Income>? response = await readRepository.GetFilter(request: request, userId: currentUser.Id);
        
        return new ResponseGetAllIncomesJson
        {
            ListAllIncomes = mapper.Map<List<ResponseIncomeShortJson>>(source: response)
        };
    }
}