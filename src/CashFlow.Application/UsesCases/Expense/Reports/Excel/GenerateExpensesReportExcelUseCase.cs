using CashFlow.Communication.Requests;
using CashFlow.Domain.Enums;
using CashFlow.Domain.Reports;
using CashFlow.Domain.Repositories.Expenses;
using ClosedXML.Excel;

namespace CashFlow.Application.UsesCases.Expense.Reports.Excel;

public class GenerateExpensesReportExcelUseCase(IExpensesReadRepository readRepository) : IGenerateExpensesReportExcelUseCase
{
    public async Task<byte[]> Execute(RequestInformationReportJson request)
    {
        List<Domain.Enitites.Expense>? response = await readRepository.GetFilter(request: request);
        if (response is null || response.Count == 0)
        {
            return [];
        }
        
        using XLWorkbook workBook = new();

        workBook.Author = "Jônatas Gonçalves de Carvalho";
        workBook.Style.Font.FontSize = 12;
        workBook.Style.Font.FontName = "Arial";

        IXLWorksheet worksheets = workBook.Worksheets.Add(sheetName: request.Date.ToString(format: "Y"));

        InsertHeader(worksheet: worksheets);

        int row = 2;
        foreach (Domain.Enitites.Expense expense in response)
        {
            worksheets.Cell(cellAddressInRange: $"A{row}").Value = expense.Title;
            worksheets.Cell(cellAddressInRange: $"B{row}").Value = expense.Date.ToString(format: "MM/dd/yyyy");
            worksheets.Cell(cellAddressInRange: $"C{row}").Value = TreatPaymentType(paymentType: expense.PaymentType);
            worksheets.Cell(cellAddressInRange: $"D{row}").Value = expense.Amount;
            worksheets.Cell(cellAddressInRange: $"D{row}").Style.NumberFormat.Format = "[$R$-pt-BR] #,##0.00";
            
            
            worksheets.Cell(cellAddressInRange: $"E{row}").Value = expense.Description;
            
            row++;
        }
        
        worksheets.Columns().AdjustToContents();

        MemoryStream file = new();
        workBook.SaveAs(stream: file);

        return file.ToArray();
    }

    private string TreatPaymentType(PaymentType paymentType) => paymentType switch
    {
        PaymentType.Cash       => ResourceReportGenerationMessage.CASH,
        PaymentType.CreditCard => ResourceReportGenerationMessage.CREDITCARD,
        PaymentType.DebitCard  => ResourceReportGenerationMessage.DEBITCARD,
        PaymentType.Electronic => ResourceReportGenerationMessage.BANKTRANSFER,
        _                      => throw new ArgumentOutOfRangeException()
    };

    private void InsertHeader(IXLWorksheet worksheet)
    {
        worksheet.Cell(cellAddressInRange: "A1").Value = ResourceReportGenerationMessage.TITLE;
        worksheet.Cell(cellAddressInRange: "B1").Value = ResourceReportGenerationMessage.DATE;
        worksheet.Cell(cellAddressInRange: "C1").Value = ResourceReportGenerationMessage.PAYMENT_TYPE;
        worksheet.Cell(cellAddressInRange: "D1").Value = ResourceReportGenerationMessage.AMOUNT;
        worksheet.Cell(cellAddressInRange: "E1").Value = ResourceReportGenerationMessage.DESCRIPTION;
        
        worksheet.Cells(cells: "A1:E1").Style.Font.Bold = true;
        worksheet.Cells(cells: "A1:E1").Style.Fill.BackgroundColor = XLColor.FromHtml(htmlColor: "#F5C2B6");
        
        worksheet.Cell(cellAddressInRange: "A1").Style.Alignment.SetHorizontal(value: XLAlignmentHorizontalValues.Center);
        worksheet.Cell(cellAddressInRange: "B1").Style.Alignment.SetHorizontal(value: XLAlignmentHorizontalValues.Center);
        worksheet.Cell(cellAddressInRange: "C1").Style.Alignment.SetHorizontal(value: XLAlignmentHorizontalValues.Center);
        worksheet.Cell(cellAddressInRange: "E1").Style.Alignment.SetHorizontal(value: XLAlignmentHorizontalValues.Center);
        worksheet.Cell(cellAddressInRange: "D1").Style.Alignment.SetHorizontal(value: XLAlignmentHorizontalValues.Right);
    }
}