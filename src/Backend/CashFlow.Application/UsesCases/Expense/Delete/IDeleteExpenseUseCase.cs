namespace CashFlow.Application.UsesCases.Expense.Delete;

public interface IDeleteExpenseUseCase
{
    Task Execute(Guid id);
}