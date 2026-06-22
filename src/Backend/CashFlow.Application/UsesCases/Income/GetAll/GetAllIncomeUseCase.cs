using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Response;
using CashFlow.Communication.Response.Income;
using CashFlow.Domain.Repositories.Incomes;
using CashFlow.Domain.Services.LoggedUser;

namespace CashFlow.Application.UsesCases.Income.GetAll;
using Domain.Enitites;

public class GetAllIncomeUseCase(IIncomesReadRepository readRepository, IMapper mapper, ILoggedUser loggedUser) : IGetAllIncomeUseCase
{
    public async Task<ResponseGetAllIncomesJson> Execute(RequestFilterJson? request)
    {
        User currentUser = await loggedUser.Get();
        List<Income>? response;

        if (request != null)
        {
            response = await readRepository.GetFilter(request: request, userId: currentUser.Id);
        }
        else
        {
            response = await readRepository.GetAllIncomes(userId: currentUser.Id);
        }
        
        return new ResponseGetAllIncomesJson
        {
            ListAllIncomes = mapper.Map<List<ResponseIncomeShortJson>>(source: response)
        };
    }
}
