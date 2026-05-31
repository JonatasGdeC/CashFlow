namespace CashFlow.Communication.Requests.Income;

public class RequestRegisterIncomeJson
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public Guid? CategoryId { get; set; }
}
