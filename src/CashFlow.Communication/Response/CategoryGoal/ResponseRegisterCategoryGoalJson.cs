namespace CashFlow.Communication.Response.CategoryGoal;

public class ResponseRegisterCategoryGoalJson
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public decimal TargetAmount { get; set; }
}
