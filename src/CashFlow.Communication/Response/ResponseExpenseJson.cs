using CashFlow.Communication.Enums;

namespace CashFlow.Communication.Response;

public class ResponseExpenseJson
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public PaymentType PaymentType { get; set; }
    public Guid? CategoryId { get; set; }
}