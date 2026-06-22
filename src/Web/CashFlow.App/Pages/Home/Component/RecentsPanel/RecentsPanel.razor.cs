using CashFlow.Communication.Requests;
using CashFlow.Communication.Response.Expense;
using CashFlow.Communication.Response.Income;
using Microsoft.AspNetCore.Components;

namespace CashFlow.App.Pages.Home.Component.RecentsPanel;

public partial class RecentsPanel
{
    [Parameter] public EventCallback<List<string>> ErrorFeedback { get; set; }
    
    private List<DashboardActivity> _activities = [];
    
    private readonly RequestFilterJson _filterRequest = new()
    {
        Date = DateOnly.FromDateTime(dateTime: DateTime.Today)
    };
    
    protected override async Task OnInitializedAsync()
    {
        await LoadActivitiesAsync();
    }
    
    private async Task LoadActivitiesAsync()
    {
        List<DashboardActivity> activities = [];

        try
        {
            ResponseGetAllExpensesJson expensesResponse = await ExpenseApiService.GetAll(request: _filterRequest);
            activities.AddRange(collection: expensesResponse.ListAllExpenses
                .Take(count: 4)
                .Select(selector: expense => new DashboardActivity(
                    Title: expense.Title,
                    Kind: "Despesa",
                    Amount: expense.Amount,
                    AmountPrefix: "-",
                    Url: $"expenses/{expense.Id}",
                    IconClass: "expense-icon",
                    AmountClass: "expense-amount",
                    CategoryId: expense.CategoryId)));
        }
        catch
        {
            await ErrorFeedback.InvokeAsync(arg: ["Nao foi possivel carregar despesas recentes."]);
        }

        try
        {
            ResponseGetAllIncomesJson incomesResponse = await IncomeApiService.GetAll(request: _filterRequest);
            activities.AddRange(collection: incomesResponse.ListAllIncomes
                .Take(count: 4)
                .Select(selector: income => new DashboardActivity(
                    Title: income.Title,
                    Kind: "Renda",
                    Amount: income.Amount,
                    AmountPrefix: "+",
                    Url: $"incomes/{income.Id}",
                    IconClass: "expense-icon income-icon",
                    AmountClass: "expense-amount income-amount",
                    CategoryId: income.CategoryId)));
        }
        catch
        {
            await ErrorFeedback.InvokeAsync(arg: ["Nao foi possivel carregar rendas recentes."]);
        }

        _activities = activities.Take(count: 6).ToList();
    }
    
    private static string GetInitial(string value)
    {
        return string.IsNullOrWhiteSpace(value: value) ? "?" : value.Trim()[index: 0].ToString().ToUpperInvariant();
    }
    
    private sealed record DashboardActivity(string Title, string Kind, decimal Amount, string AmountPrefix, string Url, string IconClass, string AmountClass, Guid? CategoryId);

}