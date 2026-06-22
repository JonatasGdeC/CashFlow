using CashFlow.App.Utils;
using CashFlow.Communication.Requests;
using Microsoft.AspNetCore.Components;

namespace CashFlow.App.Pages.Incomes.Components.IncomeReportModal;

public partial class IncomeReportModal
{
    [Parameter] public EventCallback OnClose { get; set; }

    private bool _isDownloadingPdf;
    private bool _isDownloadingExcel;
    private List<string> _listFeedbacks = [];
    private string _title = "Rendas";
    
    private readonly RequestFilterJson _reportRequest = new()
    {
        Date = DateOnly.FromDateTime(dateTime: DateTime.Today)
    };

    private bool IsDownloading => _isDownloadingPdf || _isDownloadingExcel;
    private string SelectedMonth => _reportRequest.Date.ToString(format: "yyyy-MM");

    private void HandleSelectedMonthChanged(ChangeEventArgs args)
    {
        string? value = args.Value?.ToString();
        if (DateOnly.TryParseExact(s: $"{value}-01", format: "yyyy-MM-dd", result: out DateOnly selectedDate))
        {
            _reportRequest.Date = selectedDate;
        }
    }

    private async Task DownloadPdfAsync()
    {
        _isDownloadingPdf = true;
        DownloadReport downloadReport = new(jsRuntime: JsRuntime, filterRequest: _reportRequest);
        List<string>? feedbacks = await downloadReport.Execute(generateReport: IncomeApiService.GetPdf,
            fileName: $"{_title}.pdf", contentType: DownloadReport.ContentType.Pdf);
        _isDownloadingPdf = false;
        await HandleCloseReportModal(feedbacks: feedbacks);
    }

    private async Task DownloadExcelAsync()
    {
        _isDownloadingExcel = true;
        DownloadReport downloadReport = new(jsRuntime: JsRuntime, filterRequest: _reportRequest);
        List<string>? feedbacks = await downloadReport.Execute(generateReport: IncomeApiService.GetExcel,
            fileName: $"{_title}.xlsx", contentType: DownloadReport.ContentType.Excel);
        _isDownloadingExcel = false;
        
        await HandleCloseReportModal(feedbacks: feedbacks);
    }

    private async Task HandleCloseReportModal(List<string>? feedbacks)
    {
        _listFeedbacks.Clear();
        
        if (feedbacks != null && feedbacks.Count > 0)
        {
            _listFeedbacks = feedbacks;
        }

        if (!_listFeedbacks.Any())
        {
            await OnClose.InvokeAsync();
        }
    }
}
