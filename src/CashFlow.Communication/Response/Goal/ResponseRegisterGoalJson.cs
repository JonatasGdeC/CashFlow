namespace CashFlow.Communication.Response.Goal;

public record ResponseRegisterGoalJson
{
    public Guid Id { get; init; }
    public Guid CategoryId { get; init; }
    public decimal TargetAmount { get; init; }
}
