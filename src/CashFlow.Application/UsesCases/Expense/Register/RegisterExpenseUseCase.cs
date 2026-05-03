using CashFlow.Communication.Requests;
using CashFlow.Communication.Response;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionBase;
using FluentValidation.Results;
using CashFlow.Domain.Enums;
using CashFlow.Domain.Repositories;

namespace CashFlow.Application.UsesCases.Expense.Register;

public class RegisterExpenseUseCase(IExpensesRepository repository, IUnitOfWork unitOfWork) : IRegisterExpenseUseCase
{
    public ResponseRegisterExpenseJson Execute(RequestRegisterExpenseJson request)
    {
        Validate(request: request);

        Domain.Enitites.Expense entity = new()
        {
            Id = default,
            Title = null,
            Description = null,
            Date = default,
            Amount = 0,
            PaymentType = PaymentType.Cash
        };

        repository.Add(expense: entity);
        
        
        unitOfWork.Commit();
        return new ResponseRegisterExpenseJson();
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