using System.Net.Mime;
using CashFlow.Application.UsesCases.Expense.Reports.Excel;
using CashFlow.Application.UsesCases.Expense.Reports.Pdf;
using CashFlow.Communication.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers;

[Route(template: "api/[controller]")]
[ApiController]
[Authorize]
public class ReportController : ControllerBase
{
    [HttpGet(template: "excel")]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetExcel([FromServices] IGenerateExpensesReportExcelUseCase useCase, [FromQuery] RequestFilterJson request)
    {
        byte[] file = await useCase.Execute(request: request);
        
        if (file.Length > 0)
        {
            return File(fileContents: file, contentType: MediaTypeNames.Application.Octet, fileDownloadName: "report.xlsx");
        }
        
        return NoContent();
    }
    
    [HttpGet(template: "pdf")]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetPdf([FromServices] IGenerateExpensesReportPdfUseCase useCase, [FromQuery] RequestFilterJson request)
    {
        byte[] file = await useCase.Execute(request: request);
        
        if (file.Length > 0)
        {
            return File(fileContents: file, contentType: MediaTypeNames.Application.Pdf, fileDownloadName: "report.pdf");
        }
        
        return NoContent();
    }
}