using CashFlow.Communication.Enums;

namespace CashFlow.Communication.Response.Expense;

public record ResponseExpenseJson
{
    public Guid Id { get; init; }
    public required string Title { get; init; }
    public string? Description { get; init; }
    public DateTime Date { get; init; }
    public decimal Amount { get; init; }
    public PaymentType PaymentType { get; init; }
    public Guid? CategoryId { get; init; }
}