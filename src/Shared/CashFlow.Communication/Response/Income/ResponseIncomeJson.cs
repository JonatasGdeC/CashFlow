namespace CashFlow.Communication.Response.Income;

public record ResponseIncomeJson
{
    public Guid Id { get; init; }
    public required string Title { get; init; }
    public string? Description { get; init; }
    public DateTime Date { get; init; }
    public decimal Amount { get; init; }
    public Guid? CategoryId { get; init; }
}
