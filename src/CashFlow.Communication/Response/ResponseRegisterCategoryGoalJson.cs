namespace CashFlow.Communication.Response;

public class ResponseRegisterCategoryGoalJson
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public decimal TargetAmount { get; set; }
}
