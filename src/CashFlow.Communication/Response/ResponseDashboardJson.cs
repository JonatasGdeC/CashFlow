namespace CashFlow.Communication.Response;

public class ResponseDashboardJson
{
    public Dictionary<int, decimal> AmountForMonth { get; set; } = new();
    public decimal TotalAmountForYear { get; set; }
}