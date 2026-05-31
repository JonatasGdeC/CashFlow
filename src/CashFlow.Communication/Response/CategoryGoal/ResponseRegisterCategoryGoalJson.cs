namespace CashFlow.Communication.Response.CategoryGoal;

public record ResponseRegisterCategoryGoalJson
{
    public Guid Id { get; init; }
    public Guid CategoryId { get; init; }
    public decimal TargetAmount { get; init; }
}
