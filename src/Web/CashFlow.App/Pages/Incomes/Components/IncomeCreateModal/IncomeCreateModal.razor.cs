using CashFlow.Adapter.Exceptions;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests.Income;
using CashFlow.Communication.Response.Category;
using Microsoft.AspNetCore.Components;

namespace CashFlow.App.Pages.Incomes.Components.IncomeCreateModal;

public partial class IncomeCreateModal
{
    [Parameter] public EventCallback OnIncomeCreated { get; set; }
    [Parameter] public EventCallback OnCancel { get; set; }
    
    private bool _isSubmitting;
    private List<string> _listFeedbacks = [];
    private List<ResponseCategoryShortJson> _incomeCategories = [];
    
    private readonly RequestRegisterIncomeJson _incomeModel = new()
    {
        Date = DateTime.Today
    };

    protected override async Task OnInitializedAsync()
    {
        ResponseGetAllCategoriesJson? categoriesResponse = await CategoryApiService.GetAll(categoryType: CategoryType.Income);
        _incomeCategories = categoriesResponse?.ListAllCategories ?? [];
    }

    private async Task HandleCreateIncomeAsync()
    {
        _isSubmitting = true;
        _listFeedbacks.Clear();

        try
        {
            await IncomeApiService.Register(request: _incomeModel);
            await OnIncomeCreated.InvokeAsync();
        }
        catch (ApiException exception) when (exception.ErrorMessages.Count > 0)
        {
            _listFeedbacks = exception.ErrorMessages.ToList();
        }
        catch
        {
            _listFeedbacks = ["Nao foi possivel registrar a renda."];
        }

        _isSubmitting = false;
    }
    
    private async Task CloseCreate()
    {
        if (!_isSubmitting)
        {
            await OnCancel.InvokeAsync();
        }
    }

}