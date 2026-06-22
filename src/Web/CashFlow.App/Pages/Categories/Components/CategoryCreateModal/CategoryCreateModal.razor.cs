using CashFlow.Adapter.Exceptions;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests.Category;
using CashFlow.Communication.Response.Category;
using Microsoft.AspNetCore.Components;

namespace CashFlow.App.Pages.Categories.Components.CategoryCreateModal;

public partial class CategoryCreateModal
{
    [Parameter] public EventCallback LoadCategoriesAsync { get; set; }
    [Parameter] public EventCallback OnClose { get; set; }
    [Parameter] public EventCallback<CategoryType> HandleSelectedType { get; set; }
    [Parameter] public RequestRegisterCategoryJson? CategoryModel { get; set; }
    [Parameter] public ResponseCategoryShortJson? CategoryEditModel { get; set; }
    
    private List<string> _listFeedbacks = [];
    private bool _isSubmittingCategory;

    private RequestRegisterCategoryJson _registerCategoryRequest = new();

    protected override void OnInitialized()
    {
        if (CategoryModel != null)
        {
            _registerCategoryRequest = CategoryModel;
        }

        if (CategoryEditModel != null)
        {
            _registerCategoryRequest = new RequestRegisterCategoryJson
            {
                Name = CategoryEditModel.Name,
                Description = string.Empty,
                Color = CategoryEditModel.Color,
                Type = CategoryEditModel.Type
            };
        }
    }

    private async Task HandleCreateCategoryAsync()
    {
        _isSubmittingCategory = true;
        _listFeedbacks.Clear();

        try
        {
            if (CategoryModel != null)
            {
                await CategoryApiService.Register(request: _registerCategoryRequest); 
            }

            if (CategoryEditModel != null)
            {
                await CategoryApiService.Update(id: CategoryEditModel.Id, request: _registerCategoryRequest);
            }
            
            await HandleSelectedType.InvokeAsync(arg: _registerCategoryRequest.Type);
            await OnClose.InvokeAsync();
            await LoadCategoriesAsync.InvokeAsync();
        }
        catch (ApiException exception) when (exception.ErrorMessages.Count > 0)
        {
            _listFeedbacks = exception.ErrorMessages.ToList();
        }
        catch
        {
            _listFeedbacks = ["Nao foi possivel registrar a categoria."];
        }

        _isSubmittingCategory = false;
    }
    
    private async Task CloseCategoryCreate()
    {
        if (!_isSubmittingCategory)
        {
            await OnClose.InvokeAsync();
        }
    }

    private async Task DeleteCategory()
    {
        if (CategoryEditModel != null)
        {
            await OnClose.InvokeAsync();
            await CategoryApiService.Delete(id: CategoryEditModel.Id);
            await LoadCategoriesAsync.InvokeAsync();
        }
    }
}