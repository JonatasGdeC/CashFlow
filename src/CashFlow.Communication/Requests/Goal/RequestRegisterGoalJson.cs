namespace CashFlow.Communication.Requests.Goal;

public record RequestRegisterGoalJson
{
    public decimal TargetAmount { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public Guid CategoryId { get; set; }
}
