using AutoMapper;
using CashFlow.Application.UsesCases.Income.Register;
using CashFlow.Communication.Requests;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Incomes;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using FluentValidation.Results;

namespace CashFlow.Application.UsesCases.Income.Update;

public class UpdateIncomeUseCase(
    IIncomesWriteRepository writeRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ILoggedUser loggedUser) : IUpdateIncomeUseCase
{
    public async Task Execute(Guid id, RequestRegisterIncomeJson request)
    {
        Validate(request: request);

        Domain.Enitites.User currentUser = await loggedUser.Get();
        Domain.Enitites.Income? income = await writeRepository.GetIncomeByIdToUpdate(incomeId: id, userId: currentUser.Id);

        if (income == null)
        {
            throw new NotFoundException(message: ResourceErrorMessage.INCOME_NOT_FOUND);
        }

        mapper.Map(source: request, destination: income);
        income.Id = id;

        writeRepository.Update(income: income);
        await unitOfWork.Commit();
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
