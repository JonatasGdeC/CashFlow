using System.Net.Mime;
using CashFlow.Application.UseCases.Expenses.Reports.Excel;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.API.Controllers;

[Route(template: "api/[controller]")]
[ApiController]
public class ReportController : ControllerBase
{
  [HttpGet(template: "excel")]
  [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
  [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
  public async Task<IActionResult> GetExcel([FromServices] IGenerateExpensesReportExcelUseCase useCase, [FromHeader] DateOnly month)
  {
    byte[] file = await useCase.Execute(month: month);

    if (file.Length > 0)
    {
      return File(fileContents: file, contentType: MediaTypeNames.Application.Octet, fileDownloadName: "report.xlsx");
    }

    return NoContent();
  }
}
