namespace CashFlow.Application.UsesCases.Category.Delete;

public interface IDeleteCategoryUseCase
{
    Task Execute(Guid id);
}
