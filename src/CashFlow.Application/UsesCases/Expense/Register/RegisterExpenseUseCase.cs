using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Response;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionBase;
using FluentValidation.Results;
using CashFlow.Domain.Repositories;

namespace CashFlow.Application.UsesCases.Expense.Register;

public class RegisterExpenseUseCase(IExpensesWriteRepository writeRepository, IUnitOfWork unitOfWork, IMapper mapper) : IRegisterExpenseUseCase
{
    public async Task <ResponseRegisterExpenseJson> Execute(RequestRegisterExpenseJson request)
    {
        Validate(request: request);
        
        Domain.Enitites.Expense? entity = mapper.Map<Domain.Enitites.Expense>(source: request);

        await writeRepository.Add(expense: entity);
        await unitOfWork.Commit();
        
        return mapper.Map<ResponseRegisterExpenseJson>(source: entity);
    }

    private void Validate(RequestRegisterExpenseJson request)
    {
        RegisterExpenseValidator validator = new();
        ValidationResult? result = validator.Validate(instance: request);

        if (!result.IsValid)
        {
            List<string> errors = result.Errors.Select(selector: error => error.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorsMessages: errors); 
        }
    }
}