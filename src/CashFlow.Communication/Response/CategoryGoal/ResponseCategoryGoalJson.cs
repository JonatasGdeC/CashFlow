namespace CashFlow.Communication.Response.CategoryGoal;

public record ResponseCategoryGoalJson
{
    public Guid Id { get; init; }
    public decimal TargetAmount { get; init; }
    public int Month { get; init; }
    public int Year { get; init; }
    public Guid CategoryId { get; init; }
}
