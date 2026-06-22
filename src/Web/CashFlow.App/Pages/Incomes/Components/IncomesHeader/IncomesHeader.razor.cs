using Microsoft.AspNetCore.Components;

namespace CashFlow.App.Pages.Incomes.Components.IncomesHeader;

public partial class IncomesHeader
{
    [Parameter] public EventCallback LoadIncomesAsync { get; set; }
    
    private bool _showCreate;
    private bool _showReport;

    private void OpenCreate() => _showCreate = true;
    private void CloseCreate() => _showCreate = false;

    private void OpenReport() => _showReport = true;
    private void CloseReport() => _showReport = false;
    
    private async Task HandleIncomeCreatedAsync()
    {
        _showCreate = false;
        await LoadIncomesAsync.InvokeAsync();
    }
}