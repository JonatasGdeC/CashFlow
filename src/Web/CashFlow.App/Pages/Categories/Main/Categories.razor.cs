using CashFlow.Adapter.Exceptions;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Response.Category;
using CashFlow.Communication.Response.Goal;

namespace CashFlow.App.Pages.Categories.Main;

public partial class Categories
{
    private List<string> _listFeedbacks = [];
    private CategoryType _selectedType = CategoryType.Expense;
    private bool _isLoading = true;
    private List<ResponseCategoryShortJson> _categories = [];
    private Dictionary<Guid, ResponseGoalJson> _goalsByCategory = [];
    private Dictionary<Guid, decimal> _amountsByCategory = [];

    protected override async Task OnInitializedAsync()
    {
        await LoadCategoriesAsync();
    }

    private async Task ChangeCategoryTypeAsync(CategoryType categoryType)
    {
        if (_selectedType == categoryType)
        {
            return;
        }

        _selectedType = categoryType;
        await LoadCategoriesAsync();
    }

    private async Task LoadCategoriesAsync()
    {
        _isLoading = true;
        _listFeedbacks.Clear();

        try
        {
            ResponseGetAllCategoriesJson? response = await CategoryApiService.GetAll(categoryType: _selectedType);
            _categories = response?.ListAllCategories ?? [];
            await LoadGoalsAsync();
        }
        catch (ApiException exception) when (exception.ErrorMessages.Count > 0)
        {
            _listFeedbacks = exception.ErrorMessages.ToList();
        }
        catch
        {
            _listFeedbacks = ["Nao foi possivel carregar as categorias."];
        }

        _isLoading = false;
    }

    private async Task LoadGoalsAsync()
    {
        _goalsByCategory = [];
        _amountsByCategory = [];

        foreach (ResponseCategoryShortJson category in _categories)
        {
            try
            {
                ResponseGoalJson goal = await GoalApiService.GetByCategoryId(categoryId: category.Id);
                _goalsByCategory[key: category.Id] = goal;

                RequestFilterJson filter = new()
                {
                    Date = new DateOnly(year: goal.Year, month: goal.Month, day: 1),
                    CategoryId = category.Id
                };

                decimal total = category.Type == CategoryType.Expense
                    ? (await ExpenseApiService.GetAll(request: filter)).ListAllExpenses.Sum(selector: e => e.Amount)
                    : (await IncomeApiService.GetAll(request: filter)).ListAllIncomes.Sum(selector: i => i.Amount);

                _amountsByCategory[key: category.Id] = total;
            }
            catch
            {
                // Categoria sem meta cadastrada.
            }
        }
    }

    private string GetTabClass(CategoryType categoryType)
    {
        return _selectedType == categoryType ? "segmented-button active" : "segmented-button";
    }

    private static string GetCategoryTypeTitle(CategoryType type)
    {
        return type == CategoryType.Expense ? "Categorias de despesa" : "Categorias de renda";
    }
}
