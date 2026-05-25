using CashFlow.Domain.Enums;

namespace CashFlow.Domain.Enitites;

public class Category
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required string Color { get; set; }
    public CategoryType Type { get; set; }
    
    public Guid UserId { get; set; }
    public required User User { get; set; }
}