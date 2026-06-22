using Microsoft.AspNetCore.Components;

namespace CashFlow.App.Pages.Expenses.Components.HeaderExpense;

public partial class HeaderExpense
{
    [Parameter] public EventCallback LoadExpensesAsync { get; set; }
    [Parameter] public EventCallback<List<string>> ListFeedbacks { get; set; }
    
    private bool _showCreate;
    private bool _showReport;

    private async Task OpenCreate()
    {
        await ListFeedbacks.InvokeAsync(arg: []);
        _showCreate = true;
    }

    private void CloseCreate() => _showCreate = false;

    private void OpenReport() => _showReport = true;
    private void CloseReport() => _showReport = false;

    private async Task HandleExpenseCreatedAsync()
    {
        _showCreate = false;
        await LoadExpensesAsync.InvokeAsync();
    }
}