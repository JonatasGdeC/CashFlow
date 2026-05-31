using CashFlow.Communication.Enums;

namespace CashFlow.Communication.Response.Category;

public class ResponseCategoryJson
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required string Color { get; set; }
    public CategoryType Type { get; set; }
}
