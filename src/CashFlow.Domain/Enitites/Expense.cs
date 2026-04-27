using CashFlow.Domain.Enums;

namespace CashFlow.Domain.Enitites;

public class Expense
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public PaymentType PaymentType { get; set; }
}