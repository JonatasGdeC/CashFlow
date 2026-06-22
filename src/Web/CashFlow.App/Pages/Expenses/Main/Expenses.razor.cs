using CashFlow.Adapter.Exceptions;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Response.Category;
using CashFlow.Communication.Response.Expense;
using Microsoft.AspNetCore.Components;

namespace CashFlow.App.Pages.Expenses.Main;

public partial class Expenses
{
    private bool _isLoading = true;
    private List<ResponseExpenseShortJson> _expenses = [];
    private List<string> _listFeedbacks = [];
    private List<ResponseCategoryShortJson> _categories = [];

    private readonly RequestFilterJson _filterRequest = new()
    {
        Date = DateOnly.FromDateTime(dateTime: DateTime.Today)
    };

    private string SelectedMonth => _filterRequest.Date.ToString(format: "yyyy-MM");

    protected override async Task OnInitializedAsync()
    {
        await Task.WhenAll(LoadCategoriesAsync(), LoadExpensesAsync());
    }

    private async Task LoadCategoriesAsync()
    {
        try
        {
            ResponseGetAllCategoriesJson? response = await CategoryApiService.GetAll(categoryType: CategoryType.Expense);
            _categories = response?.ListAllCategories ?? [];
        }
        catch
        {
            _categories = [];
        }
    }

    private async Task HandleSelectedMonthChanged(ChangeEventArgs args)
    {
        string? value = args.Value?.ToString();
        if (DateOnly.TryParseExact(s: $"{value}-01", format: "yyyy-MM-dd", result: out DateOnly selectedDate))
        {
            _filterRequest.Date = selectedDate;
            await LoadExpensesAsync();
        }
    }

    private async Task HandleTitleChanged(ChangeEventArgs args)
    {
        string? value = args.Value?.ToString();
        _filterRequest.Title = string.IsNullOrWhiteSpace(value: value) ? null : value.Trim();
        await LoadExpensesAsync();
    }

    private async Task HandleAmountChanged(ChangeEventArgs args)
    {
        string? value = args.Value?.ToString();
        _filterRequest.Amount = decimal.TryParse(s: value, result: out decimal parsed) ? parsed : null;
        await LoadExpensesAsync();
    }

    private async Task HandleCategoryChanged(ChangeEventArgs args)
    {
        string? value = args.Value?.ToString();
        _filterRequest.CategoryId = Guid.TryParse(input: value, result: out Guid parsed) ? parsed : null;
        await LoadExpensesAsync();
    }

    private async Task ClearFiltersAsync()
    {
        _filterRequest.Title = null;
        _filterRequest.Amount = null;
        _filterRequest.CategoryId = null;
        _filterRequest.Date = DateOnly.FromDateTime(dateTime: DateTime.Today);
        await LoadExpensesAsync();
    }

    private async Task LoadExpensesAsync()
    {
        _isLoading = true;
        _expenses.Clear();
        _listFeedbacks.Clear();

        try
        {
            ResponseGetAllExpensesJson response = await ExpenseApiService.GetAll(request: _filterRequest);
            _expenses = response.ListAllExpenses;
        }
        catch (ApiException exception) when (exception.ErrorMessages.Count > 0)
        {
            _listFeedbacks = exception.ErrorMessages.ToList();
        }
        catch
        {
            _listFeedbacks = ["Nao foi possivel carregar suas despesas."];
        }

        _isLoading = false;
    }
}
