namespace CashFlow.Communication.Response;

public class ResponseCategoryGoalJson
{
    public Guid Id { get; set; }
    public decimal TargetAmount { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public Guid CategoryId { get; set; }
}
