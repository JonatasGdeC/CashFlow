namespace CashFlow.Communication.Response.Income;

public class ResponseIncomeShortJson
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public decimal Amount { get; set; }
    public Guid? CategoryId { get; set; }
}
