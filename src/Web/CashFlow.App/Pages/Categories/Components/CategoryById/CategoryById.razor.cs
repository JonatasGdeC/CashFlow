using CashFlow.Communication.Response.Category;
using Microsoft.AspNetCore.Components;

namespace CashFlow.App.Pages.Categories.Components.CategoryById;

public partial class CategoryById
{
    [Parameter] public Guid? CategoryId { get; set; }
    
    private ResponseCategoryJson? _category;

    protected override async Task OnParametersSetAsync()
    {
        if (CategoryId.HasValue)
        {
            _category = await CategoryApiService.GetById(id: CategoryId.Value);
        }
    }
}