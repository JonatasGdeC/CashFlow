using CashFlow.Domain.Entities;
using CashFlow.Domain.Reports;
using CashFlow.Domain.Repositories.Expenses;
using ClosedXML.Excel;

namespace CashFlow.Application.UseCases.Expenses.Reports.Excel;

public class GenerateExpensesReportExcelUseCase : IGenerateExpensesReportExcelUseCase
{
  private readonly IExpensesReadOnlyRepository _repository;

  public GenerateExpensesReportExcelUseCase(IExpensesReadOnlyRepository repository)
  {
    _repository = repository;
  }

  public async Task<byte[]> Execute(DateOnly month)
  {
    List<Expense> expenses = await _repository.FilterByMonth(date: month);

    XLWorkbook workbook = new XLWorkbook();

    workbook.Author = "Jônatas Carvalho";
    workbook.Style.Font.FontSize = 12;
    workbook.Style.Font.FontName = "Arial";

    IXLWorksheet worksheet = workbook.Worksheets.Add(sheetName: month.ToString(format: "Y"));
    InsertHeader(worksheet: worksheet);

    MemoryStream file = new MemoryStream();
    workbook.SaveAs(stream: file);

    return file.ToArray();
  }

  private void InsertHeader(IXLWorksheet worksheet)
  {
    worksheet.Cell(cellAddressInRange: "A1").Value = ResourcesReportsGeneretionMessage.TITLE;
    worksheet.Cell(cellAddressInRange: "B1").Value = ResourcesReportsGeneretionMessage.DATE;
    worksheet.Cell(cellAddressInRange: "C1").Value = ResourcesReportsGeneretionMessage.PAYMENT_TYPE;
    worksheet.Cell(cellAddressInRange: "D1").Value = ResourcesReportsGeneretionMessage.AMOUNT;
    worksheet.Cell(cellAddressInRange: "E1").Value = ResourcesReportsGeneretionMessage.DESCRIPTION;

    worksheet.Cells(cells: "A1:E1").Style.Font.Bold = true;
    worksheet.Cells(cells: "A1:E1").Style.Fill.BackgroundColor = XLColor.FromHtml(htmlColor: "#F5C2B6");

    worksheet.Cell(cellAddressInRange: "A1").Style.Alignment.SetHorizontal(value: XLAlignmentHorizontalValues.Center);
    worksheet.Cell(cellAddressInRange: "B1").Style.Alignment.SetHorizontal(value: XLAlignmentHorizontalValues.Center);
    worksheet.Cell(cellAddressInRange: "C1").Style.Alignment.SetHorizontal(value: XLAlignmentHorizontalValues.Center);
    worksheet.Cell(cellAddressInRange: "D1").Style.Alignment.SetHorizontal(value: XLAlignmentHorizontalValues.Right);
    worksheet.Cell(cellAddressInRange: "E1").Style.Alignment.SetHorizontal(value: XLAlignmentHorizontalValues.Center);
  }
}
