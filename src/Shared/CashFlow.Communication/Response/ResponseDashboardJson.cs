namespace CashFlow.Communication.Response;

public record ResponseDashboardJson
{
    public Dictionary<int, decimal> AmountForMonth { get; init; } = new();
    public decimal TotalAmountForYear { get; init; }
}