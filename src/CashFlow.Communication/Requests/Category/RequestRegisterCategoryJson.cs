using CashFlow.Communication.Enums;

namespace CashFlow.Communication.Requests.Category;

public class RequestRegisterCategoryJson
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Color { get; set; } = string.Empty;
    public CategoryType Type { get; set; }
}
