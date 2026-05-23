using AutoMapper;
using CashFlow.Communication.Response;
using CashFlow.Domain.Services.LoggedUser;

namespace CashFlow.Application.UsesCases.User.Get;

public class GetUserProfileUseCase(ILoggedUser loggedUser, IMapper mapper) : IGetUserProfileUseCase
{
    public async Task<ResponseUserProfileJson> Execute()
    {
        Domain.Enitites.User user = await loggedUser.Get();
        return mapper.Map<ResponseUserProfileJson>(source: user); 
    }
}