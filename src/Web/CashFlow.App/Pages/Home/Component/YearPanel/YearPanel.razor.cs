using CashFlow.Communication.Response;
using Microsoft.AspNetCore.Components;

namespace CashFlow.App.Pages.Home.Component.YearPanel;

public partial class YearPanel
{
    [Parameter] public ResponseDashboardJson? ExpenseDashboard { get; set; }
    [Parameter] public ResponseDashboardJson? IncomeDashboard { get; set; }
    
    private decimal YearExpenses => ExpenseDashboard?.TotalAmountForYear ?? 0;
    private decimal YearIncomes => IncomeDashboard?.TotalAmountForYear ?? 0;
}