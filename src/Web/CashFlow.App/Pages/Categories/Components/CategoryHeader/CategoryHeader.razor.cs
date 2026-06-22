using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests.Category;
using Microsoft.AspNetCore.Components;

namespace CashFlow.App.Pages.Categories.Components.CategoryHeader;

public partial class CategoryHeader
{
    [Parameter] public EventCallback<CategoryType> HandleSelectedType { get; set; }
    [Parameter] public EventCallback LoadCategoriesAsync { get; set; }
    [Parameter] public CategoryType SelectedType { get; set; }
    
    private bool _showCategoryCreate;
    
    private readonly RequestRegisterCategoryJson _categoryModel = new()
    {
        Color = "#ff493f",
        Type = CategoryType.Expense
    };
    
    private void OpenCategoryCreate()
    {
        _categoryModel.Name = string.Empty;
        _categoryModel.Description = null;
        _categoryModel.Color = SelectedType == CategoryType.Expense ? "#ff493f" : "#15803d";
        _categoryModel.Type = SelectedType;
        _showCategoryCreate = true;
    }
    
    private void CloseCategoryCreate() => _showCategoryCreate = false;
}