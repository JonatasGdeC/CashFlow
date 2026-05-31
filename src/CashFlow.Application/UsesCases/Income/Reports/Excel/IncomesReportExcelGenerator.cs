using CashFlow.Application.Utils.GenerateReportExcel;
using CashFlow.Communication.Requests;
using CashFlow.Domain.Reports;
using ClosedXML.Excel;

namespace CashFlow.Application.UsesCases.Income.Reports.Excel;

public class IncomesReportExcelGenerator(RequestFilterJson request, List<Domain.Enitites.Income> incomes) : GenerateReportExcel
{
    protected override void InsertHeader(IXLWorksheet worksheet)
    {
        worksheet.Cell(cellAddressInRange: "A1").Value = ResourceReportGenerationMessage.TITLE;
        worksheet.Cell(cellAddressInRange: "B1").Value = ResourceReportGenerationMessage.DATE;
        worksheet.Cell(cellAddressInRange: "C1").Value = ResourceReportGenerationMessage.AMOUNT;
        worksheet.Cell(cellAddressInRange: "D1").Value = ResourceReportGenerationMessage.DESCRIPTION;
        
        worksheet.Cells(cells: "A1:D1").Style.Font.Bold = true;
        worksheet.Cells(cells: "A1:D1").Style.Fill.BackgroundColor = XLColor.FromHtml(htmlColor: "#F5C2B6");
        
        worksheet.Cell(cellAddressInRange: "A1").Style.Alignment.SetHorizontal(value: XLAlignmentHorizontalValues.Center);
        worksheet.Cell(cellAddressInRange: "B1").Style.Alignment.SetHorizontal(value: XLAlignmentHorizontalValues.Center);
        worksheet.Cell(cellAddressInRange: "C1").Style.Alignment.SetHorizontal(value: XLAlignmentHorizontalValues.Right);
        worksheet.Cell(cellAddressInRange: "D1").Style.Alignment.SetHorizontal(value: XLAlignmentHorizontalValues.Left);
    }

    protected override void InsertBody(IXLWorksheet worksheet)
    {
        int row = 2;

        foreach (Domain.Enitites.Income expense in incomes)
        {
            worksheet.Cell(cellAddressInRange: $"A{row}").Value = expense.Title;
            worksheet.Cell(cellAddressInRange: $"B{row}").Value = expense.Date.ToString(format: "MM/dd/yyyy");
            
            worksheet.Cell(cellAddressInRange: $"C{row}").Value = expense.Amount;
            worksheet.Cell(cellAddressInRange: $"C{row}").Style.NumberFormat.Format = "[$R$-pt-BR] #,##0.00";
            
            worksheet.Cell(cellAddressInRange: $"D{row}").Value = expense.Description;
        
            row++;
        }
    }

    public byte[] Execute() => Build(request: request);
}