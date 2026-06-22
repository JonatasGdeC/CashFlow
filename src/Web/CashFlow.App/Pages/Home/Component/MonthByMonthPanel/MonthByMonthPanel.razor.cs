using CashFlow.Communication.Response;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CashFlow.App.Pages.Home.Component.MonthByMonthPanel;

public partial class MonthByMonthPanel
{
    [Parameter] public ResponseDashboardJson? ExpenseDashboard { get; set; }
    [Parameter] public ResponseDashboardJson? IncomeDashboard { get; set; }

    private readonly string _chartElementId = $"monthly-comparison-{Guid.NewGuid():N}";
    private bool _shouldRenderChart = true;

    private IEnumerable<DashboardMonth> MonthlySummary =>
        Enumerable.Range(start: 1, count: 12)
            .Select(selector: month => new DashboardMonth(
                Label: new DateTime(year: DateTime.Today.Year, month: month, day: 1).ToString(format: "MMM"),
                Incomes: GetMonthAmount(dashboard: IncomeDashboard, month: month),
                Expenses: GetMonthAmount(dashboard: ExpenseDashboard, month: month)));

    protected override void OnParametersSet()
    {
        _shouldRenderChart = true;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!_shouldRenderChart)
        {
            return;
        }

        _shouldRenderChart = false;
        DashboardMonth[] months = MonthlySummary.ToArray();

        await JsRuntime.InvokeVoidAsync(
            identifier: "cashFlowCharts.renderMonthlyComparison",
            _chartElementId,
            months.Select(selector: month => month.Label).ToArray(),
            months.Select(selector: month => month.Expenses).ToArray(),
            months.Select(selector: month => month.Incomes).ToArray());
    }

    private static decimal GetMonthAmount(ResponseDashboardJson? dashboard, int month)
    {
        return dashboard?.AmountForMonth.GetValueOrDefault(key: month) ?? 0;
    }

    private sealed record DashboardMonth(string Label, decimal Incomes, decimal Expenses);
}
