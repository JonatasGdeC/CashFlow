using AutoMapper;
using CashFlow.Communication.Response;
using CashFlow.Domain.Repositories.Incomes;
using CashFlow.Domain.Services.LoggedUser;

namespace CashFlow.Application.UsesCases.Income.GetAll;

public class GetAllIncomeUseCase(IIncomesReadRepository readRepository, IMapper mapper, ILoggedUser loggedUser) : IGetAllIncomeUseCase
{
    public async Task<ResponseGetAllIncomesJson> Execute()
    {
        Domain.Enitites.User currentUser = await loggedUser.Get();
        List<Domain.Enitites.Income>? response = await readRepository.GetAllIncomes(userId: currentUser.Id);

        return new ResponseGetAllIncomesJson
        {
            ListAllIncomes = mapper.Map<List<ResponseIncomeShortJson>>(source: response)
        };
    }
}
