using CashFlow.Adapter.Exceptions;
using CashFlow.Communication.Requests;
using Microsoft.JSInterop;

namespace CashFlow.App.Utils;

public class DownloadReport(IJSRuntime jsRuntime, RequestFilterJson filterRequest)
{
    public async Task<List<string>?> Execute(Func<RequestFilterJson, Task<byte[]?>> generateReport, string fileName, ContentType contentType, string emptyMessage =  "Nao ha dados para gerar o relatorio nesse periodo.")
    {
        List<string>? listFeedbacks = null;
        
        try
        {
            byte[]? file = await generateReport(arg: filterRequest);

            if (file is null || file.Length == 0)
            {
                listFeedbacks = [emptyMessage];
                return listFeedbacks;
            }

            await jsRuntime.InvokeVoidAsync(
                identifier: "cashFlowFiles.download",
                fileName, HandleContentType(contentType: contentType), 
                Convert.ToBase64String(inArray: file));
        }
        catch (ApiException exception) when (exception.ErrorMessages.Count > 0)
        {
            listFeedbacks = exception.ErrorMessages.ToList();
        }
        catch
        {
            listFeedbacks = ["Nao foi possivel gerar o relatorio."];
        }
        
        return listFeedbacks;
    }
    
    public enum ContentType
    {
        Excel = 0,
        Pdf = 1
    }

    private string HandleContentType(ContentType contentType) => contentType switch
    {
        ContentType.Excel => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        ContentType.Pdf   => "application/pdf",
        _                 => string.Empty
    };
}