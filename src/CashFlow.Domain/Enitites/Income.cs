namespace CashFlow.Domain.Enitites;

public class Income
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    
    public Guid? CategoryId { get; set; }
    public Category? Category { get; set; }
    
    public Guid UserId { get; set; }
    public required User User { get; set; }
}