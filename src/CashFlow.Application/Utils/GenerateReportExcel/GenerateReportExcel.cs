using CashFlow.Communication.Requests;
using ClosedXML.Excel;

namespace CashFlow.Application.Utils.GenerateReportExcel;

public abstract class GenerateReportExcel
{
    protected abstract void InsertHeader(IXLWorksheet worksheet);
    protected abstract void InsertBody(IXLWorksheet worksheet);

    protected byte[] Build(RequestFilterJson request)
    {
        using XLWorkbook workBook = new();

        workBook.Author = "Jônatas Gonçalves de Carvalho";
        workBook.Style.Font.FontSize = 12;
        workBook.Style.Font.FontName = "Arial";

        IXLWorksheet worksheets = workBook.Worksheets.Add(sheetName: request.Date.ToString(format: "Y"));
        
        InsertHeader(worksheet: worksheets);
        InsertBody(worksheet: worksheets);
        
        worksheets.Columns().AdjustToContents();
        
        MemoryStream file = new();
        workBook.SaveAs(stream: file);

        return file.ToArray();
    }
}