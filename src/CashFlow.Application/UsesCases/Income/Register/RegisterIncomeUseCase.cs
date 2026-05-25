using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Response;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Incomes;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception.ExceptionBase;
using FluentValidation.Results;

namespace CashFlow.Application.UsesCases.Income.Register;

public class RegisterIncomeUseCase(IIncomesWriteRepository writeRepository, IUnitOfWork unitOfWork, IMapper mapper, ILoggedUser loggedUser) : IRegisterIncomeUseCase
{
    public async Task<ResponseRegisterIncomeJson> Execute(RequestRegisterIncomeJson request)
    {
        Validate(request: request);

        Domain.Enitites.User currentUser = await loggedUser.Get();
        Domain.Enitites.Income income = mapper.Map<Domain.Enitites.Income>(source: request);
        income.UserId = currentUser.Id;

        await writeRepository.Add(income: income);
        await unitOfWork.Commit();

        return mapper.Map<ResponseRegisterIncomeJson>(source: income);
    }

    private void Validate(RequestRegisterIncomeJson request)
    {
        RegisterIncomeValidator validator = new();
        ValidationResult result = validator.Validate(instance: request);

        if (!result.IsValid)
        {
            List<string> errors = result.Errors.Select(selector: error => error.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorsMessages: errors);
        }
    }
}
