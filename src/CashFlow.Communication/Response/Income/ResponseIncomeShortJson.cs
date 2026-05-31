namespace CashFlow.Communication.Response.Income;

public record ResponseIncomeShortJson
{
    public Guid Id { get; init; }
    public required string Title { get; init; }
    public decimal Amount { get; init; }
    public Guid? CategoryId { get; init; }
}
