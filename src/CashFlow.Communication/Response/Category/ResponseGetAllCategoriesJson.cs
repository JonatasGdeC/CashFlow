namespace CashFlow.Communication.Response.Category;

public record ResponseGetAllCategoriesJson
{
    public List<ResponseCategoryShortJson> ListAllCategories { get; init; } = [];
}
