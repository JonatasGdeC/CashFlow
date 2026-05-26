namespace CashFlow.Application.UsesCases.CategoryGoal.Delete;

public interface IDeleteCategoryGoalUseCase
{
    Task Execute(Guid id);
}
