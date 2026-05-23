using AutoMapper;
using CashFlow.Application.UsesCases.User.Register;
using CashFlow.Communication.Requests;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Users;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception.ExceptionBase;
using FluentValidation.Results;

namespace CashFlow.Application.UsesCases.User.Update;

public class UpdateUserUseCase(ILoggedUser loggedUser, IMapper mapper, IUsersWriteRepository writeRepository, IUnitOfWork unitOfWork) : IUpdateUserUseCase
{
    public async Task Execute(RequestUpdateUserJson request)
    {
        Validate(request: request);
        Domain.Enitites.User user = await loggedUser.Get();
        
        mapper.Map(source: request, destination: user);
        
        writeRepository.Update(user: user);
        await unitOfWork.Commit();
    }
    
    private void Validate(RequestUpdateUserJson request)
    {
        ValidationResult resultRequest = new RegisterUserValidator().Validate(instance: request);


        if (!resultRequest.IsValid)
        {
            List<string> errors = resultRequest.Errors.Select(selector: error => error.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorsMessages: errors);
        }
    }
}