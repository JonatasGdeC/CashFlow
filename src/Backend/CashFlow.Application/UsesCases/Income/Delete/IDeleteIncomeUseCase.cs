namespace CashFlow.Application.UsesCases.Income.Delete;

public interface IDeleteIncomeUseCase
{
    Task Execute(Guid id);
}
