namespace CashFlow.Communication.Response;

public class ResponseIncomeJson
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public Guid? CategoryId { get; set; }
}
