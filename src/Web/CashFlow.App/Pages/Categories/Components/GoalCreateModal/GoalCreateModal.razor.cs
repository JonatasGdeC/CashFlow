using CashFlow.Adapter.Exceptions;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests.Goal;
using CashFlow.Communication.Response.Category;
using Microsoft.AspNetCore.Components;

namespace CashFlow.App.Pages.Categories.Components.GoalCreateModal;

public partial class GoalCreateModal
{
    [Parameter] public RequestRegisterGoalJson? GoalModel { get; set; }
    [Parameter] public ResponseCategoryShortJson? SelectedCategoryForGoal { get; set; }
    [Parameter] public Guid? EditingGoalId { get; set; }
    [Parameter] public EventCallback LoadCategoriesGoalsAsync { get; set; }
    [Parameter] public EventCallback OnClose { get; set; }
    
    private bool _isSubmittingGoal;
    private List<string> _listFeedbacks = [];
    
    private async Task HandleSaveGoalAsync()
    {
        _isSubmittingGoal = true;
        _listFeedbacks.Clear();

        try
        {
            if (EditingGoalId is Guid goalId)
            {
                await GoalApiService.Update(id: goalId, request: GoalModel!);
            }
            else
            {
                await GoalApiService.Register(request: GoalModel!);
            }

            await OnClose.InvokeAsync();
            await LoadCategoriesGoalsAsync.InvokeAsync();
        }
        catch (ApiException exception) when (exception.ErrorMessages.Count > 0)
        {
            _listFeedbacks = exception.ErrorMessages.ToList();
        }
        catch
        {
            _listFeedbacks = ["Nao foi possivel salvar a meta."];
        }

        _isSubmittingGoal = false;
    }
    
    private async Task CloseGoalModal()
    {
        if (!_isSubmittingGoal)
        {
            await OnClose.InvokeAsync();
        }
    }
    
    private static string GetGoalMeaning(CategoryType type)
    {
        return type == CategoryType.Expense
            ? "Limite maximo que voce pretende gastar."
            : "Objetivo que voce pretende juntar.";
    }
}