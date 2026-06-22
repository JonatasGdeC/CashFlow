using CashFlow.Communication.Enums;

namespace CashFlow.Communication.Response.Category;

public record ResponseCategoryJson
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
    public required string Color { get; init; }
    public CategoryType Type { get; init; }
}
