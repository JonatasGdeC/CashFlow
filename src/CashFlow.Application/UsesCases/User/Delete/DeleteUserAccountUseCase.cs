using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Users;
using CashFlow.Domain.Services.LoggedUser;

namespace CashFlow.Application.UsesCases.User.Delete;

public class DeleteUserAccountUseCase(
    ILoggedUser loggedUser,
    IUsersWriteRepository writeRepository,
    IUnitOfWork unitOfWork) : IDeleteUserAccountUseCase
{
    public async Task Execute()
    {
        Domain.Enitites.User user = await loggedUser.Get();
        writeRepository.Delete(user: user);
        await unitOfWork.Commit();
    }
}