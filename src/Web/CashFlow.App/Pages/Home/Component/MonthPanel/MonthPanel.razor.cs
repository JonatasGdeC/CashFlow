using CashFlow.Communication.Response;
using Microsoft.AspNetCore.Components;

namespace CashFlow.App.Pages.Home.Component.MonthPanel;

public partial class MonthPanel
{
    [Parameter] public ResponseDashboardJson? ExpenseDashboard { get; set; }
    [Parameter] public ResponseDashboardJson? IncomeDashboard { get; set; }
    
    private decimal MonthBalance => MonthIncomes - MonthExpenses;
    private string ExpenseProgressStyle => $"--value: {ExpenseProgressPercentage:0.##}%; margin: 12px 0 16px;";
    
    private decimal MonthExpenses => GetMonthAmount(dashboard: ExpenseDashboard, month: DateTime.Today.Month);
    private decimal MonthIncomes => GetMonthAmount(dashboard: IncomeDashboard, month: DateTime.Today.Month);
    
    private decimal ExpenseProgressPercentage
    {
        get
        {
            if (MonthIncomes <= 0)
            {
                return MonthExpenses > 0 ? 100 : 0;
            }

            return Math.Min(val1: 100, val2: MonthExpenses / MonthIncomes * 100);
        }
    }
    
    private static decimal GetMonthAmount(ResponseDashboardJson? dashboard, int month)
    {
        return dashboard?.AmountForMonth.GetValueOrDefault(key: month) ?? 0;
    }
}