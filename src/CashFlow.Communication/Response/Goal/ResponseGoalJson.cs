namespace CashFlow.Communication.Response.Goal;

public record ResponseGoalJson
{
    public Guid Id { get; init; }
    public decimal TargetAmount { get; init; }
    public int Month { get; init; }
    public int Year { get; init; }
    public Guid CategoryId { get; init; }
}
