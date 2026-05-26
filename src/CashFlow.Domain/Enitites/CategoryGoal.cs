namespace CashFlow.Domain.Enitites;

public class CategoryGoal
{
    public Guid Id { get; set; }
    public decimal TargetAmount { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }

    public Guid CategoryId { get; set; }
    public required Category Category { get; set; }

    public Guid UserId { get; set; }
    public required User User { get; set; }
}