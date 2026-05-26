namespace CashFlow.Communication.Requests;

public class RequestFilterJson
{
    public DateOnly Date { get; set; }
    public Guid? CategoryId { get; set; }
    public string? Title { get; set; }
    public decimal? Amount { get; set; }
}