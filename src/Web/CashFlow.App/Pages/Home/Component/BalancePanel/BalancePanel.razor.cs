using CashFlow.Communication.Response;
using CashFlow.Communication.Response.User;
using Microsoft.AspNetCore.Components;

namespace CashFlow.App.Pages.Home.Component.BalancePanel;

public partial class BalancePanel
{
    [Parameter] public ResponseDashboardJson? ExpenseDashboard { get; set; }
    [Parameter] public ResponseDashboardJson? IncomeDashboard { get; set; }
    [Parameter] public EventCallback<List<string>> ErrorFeedback { get; set; }
    
    private string _userName = "Usuario";
    private string _userEmail = "perfil nao carregado";
    
    private string UserName => _userName;
    private string UserEmail => _userEmail;
    private string UserInitials => BuildInitials(name: _userName);
    private decimal YearBalance => YearIncomes - YearExpenses;
    
    private decimal YearExpenses => ExpenseDashboard?.TotalAmountForYear ?? 0;
    private decimal YearIncomes => IncomeDashboard?.TotalAmountForYear ?? 0;
    
    protected override async Task OnInitializedAsync()
    {
        await LoadProfileAsync();
    }
    
    private async Task LoadProfileAsync()
    {
        try
        {
            ResponseUserProfileJson profile = await UserApiService.GetProfile();
            _userName = string.IsNullOrWhiteSpace(value: profile.Name) ? "Usuario" : profile.Name;
            _userEmail = string.IsNullOrWhiteSpace(value: profile.Email) ? "perfil nao carregado" : profile.Email;
        }
        catch
        {
            await ErrorFeedback.InvokeAsync(arg: ["Nao foi possivel carregar os dados do perfil."]);
        }
    }
    
    private static string BuildInitials(string name)
    {
        string[] parts = name.Split(separator: ' ', options: StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        return parts.Length switch
        {
            0 => "?",
            1 => parts[0][index: 0].ToString().ToUpperInvariant(),
            _ => $"{parts[0][index: 0]}{parts[^1][index: 0]}".ToUpperInvariant()
        };
    }
}