namespace CashFlow.Application.UsesCases.Goal.Delete;

public interface IDeleteGoalUseCase
{
    Task Execute(Guid id);
}
