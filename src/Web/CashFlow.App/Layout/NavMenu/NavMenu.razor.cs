using CashFlow.Communication.Response;

namespace CashFlow.App.Layout.NavMenu;

public partial class NavMenu
{
    private static readonly string[] ScoreEmojis = ["🤑", "😁", "😃", "🙂", "🧐", "😐", "🫤", "☹️", "😖", "😭"];

    private bool _collapseNavMenu = true;
    private string _statusEmoji = "😐";

    private string? NavMenuCssClass => _collapseNavMenu ? "collapse" : null;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            ResponseDashboardJson expenseDashboard = await ExpenseApiService.GetDashboard();
            ResponseDashboardJson incomeDashboard = await IncomeApiService.GetDashboard();

            decimal totalExpenses = expenseDashboard.TotalAmountForYear;
            decimal totalIncomes = incomeDashboard.TotalAmountForYear;

            int score;
            if (totalIncomes == 0 && totalExpenses == 0)
            {
                score = 5;
            }
            else
            {
                decimal savingsRate = totalIncomes == 0 ? -1m : (totalIncomes - totalExpenses) / totalIncomes;

                score = (int)Math.Round(d: savingsRate * 5 + 5);
                score = Math.Clamp(value: score, min: 1, max: 10);
            }

            _statusEmoji = ScoreEmojis[10 - score];
        }
        catch
        {
            _statusEmoji = "😐";
        }
    }

    private void ToggleNavMenu()
    {
        _collapseNavMenu = !_collapseNavMenu;
    }

    private async Task HandleLogoutAsync()
    {
        await CookieAuthenticationStateProvider.RemoveTokenAsync();
    }
}