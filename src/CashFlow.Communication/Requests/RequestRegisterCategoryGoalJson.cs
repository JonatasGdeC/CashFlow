namespace CashFlow.Communication.Requests;

public class RequestRegisterCategoryGoalJson
{
    public decimal TargetAmount { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public Guid CategoryId { get; set; }
}
