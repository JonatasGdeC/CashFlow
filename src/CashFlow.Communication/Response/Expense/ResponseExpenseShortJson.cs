namespace CashFlow.Communication.Response.Expense;

public record ResponseExpenseShortJson
{
    public Guid Id { get; init; }
    public required string Title { get; init; }
    public decimal Amount { get; init; }
    public Guid? CategoryId { get; init; }
}