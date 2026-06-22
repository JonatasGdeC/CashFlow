using CashFlow.Adapter.Exceptions;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests.Goal;
using CashFlow.Communication.Response.Category;
using CashFlow.Communication.Response.Goal;
using Microsoft.AspNetCore.Components;

namespace CashFlow.App.Pages.Categories.Components.CategoryList;

public partial class CategoryList
{
    [Parameter] public List<ResponseCategoryShortJson> Categories { get; set; } = [];
    [Parameter] public Dictionary<Guid, ResponseGoalJson> GoalsByCategory { get; set; } = [];
    [Parameter] public Dictionary<Guid, decimal> AmountsByCategory { get; set; } = [];
    [Parameter] public EventCallback LoadCategoriesGoalsAsync { get; set; }
    
    private Guid? _editingGoalId;
    private ResponseCategoryShortJson? _selectedCategoryForGoal;
    private bool _showGoalModal;
    private bool _showCategoryCreate;
    private List<string> _listFeedbacks = [];
    
    private readonly RequestRegisterGoalJson _goalModel = new()
    {
        Month = DateTime.Today.Month,
        Year = DateTime.Today.Year
    };
    
    private void OpenGoalModal(ResponseCategoryShortJson category)
    {
        _selectedCategoryForGoal = category;

        if (GoalsByCategory.TryGetValue(key: category.Id, value: out ResponseGoalJson? goal))
        {
            _editingGoalId = goal.Id;
            _goalModel.TargetAmount = goal.TargetAmount;
            _goalModel.Month = goal.Month;
            _goalModel.Year = goal.Year;
            _goalModel.CategoryId = goal.CategoryId;
        }
        else
        {
            _editingGoalId = null;
            _goalModel.TargetAmount = 0;
            _goalModel.Month = DateTime.Today.Month;
            _goalModel.Year = DateTime.Today.Year;
            _goalModel.CategoryId = category.Id;
        }

        _showGoalModal = true;
    }

    private void OpenEditCategoryModal(ResponseCategoryShortJson category)
    {
        _selectedCategoryForGoal = category;
        _showCategoryCreate = true;
    }
    
    private void CloseGoalModal() => _showGoalModal = false;
    private void CloseEditModal() => _showCategoryCreate = false;
    
    private async Task HandleDeleteGoalAsync(ResponseGoalJson goal)
    {
        try
        {
            await GoalApiService.Delete(id: goal.Id);
            await LoadCategoriesGoalsAsync.InvokeAsync();
        }
        catch (ApiException exception) when (exception.ErrorMessages.Count > 0)
        {
            _listFeedbacks = exception.ErrorMessages.ToList();
        }
        catch
        {
            _listFeedbacks = ["Nao foi possivel remover a meta."];
        }
    }
    
    private static string GetGoalText(ResponseGoalJson? goal, CategoryType type)
    {
        if (goal is null)
        {
            return "Sem meta cadastrada";
        }

        string meaning = type == CategoryType.Expense ? "limite" : "objetivo";
        return $"{meaning} de {goal.TargetAmount:C} em {goal.Month:00}/{goal.Year}";
    }

    private static string NormalizeColor(string color)
    {
        return string.IsNullOrWhiteSpace(value: color) ? "#ff493f" : color;
    }
    
    private ResponseGoalJson? GetGoal(Guid categoryId)
    {
        return GoalsByCategory.GetValueOrDefault(key: categoryId);
    }

    private int GetProgressPercentage(ResponseGoalJson goal, Guid categoryId)
    {
        if (goal.TargetAmount <= 0)
            return 0;

        decimal spent = AmountsByCategory.GetValueOrDefault(key: categoryId);
        int percentage = (int)Math.Round(d: spent / goal.TargetAmount * 100);
        return Math.Clamp(value: percentage, min: 0, max: 100);
    }

    private decimal GetCategoryAmount(Guid categoryId)
    {
        return AmountsByCategory.GetValueOrDefault(key: categoryId);
    }
}