namespace CashFlow.Communication.Response;

public class ResponseExpenseShortJson
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public decimal Amount { get; set; }
}