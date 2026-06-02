using AutoMapper;
using CashFlow.Communication.Response;
using CashFlow.Communication.Response.Income;
using CashFlow.Domain.Repositories.Incomes;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UsesCases.Income.GetById;

public class GetByIdIncomeUseCase(IIncomesReadRepository readRepository, IMapper mapper, ILoggedUser loggedUser) : IGetByIdIncomeUseCase
{
    public async Task<ResponseIncomeJson> Execute(Guid id)
    {
        Domain.Enitites.User currentUser = await loggedUser.Get();
        Domain.Enitites.Income? response = await readRepository.GetIncomeById(incomeId: id, userId: currentUser.Id);

        if (response != null)
        {
            return mapper.Map<ResponseIncomeJson>(source: response);
        }

        throw new NotFoundException(message: ResourceErrorMessage.INCOME_NOT_FOUND);
    }
}
