using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers;

[Route(template: "api/[controller]")]
[ApiController]
public class ReportController : ControllerBase
{
    [HttpGet(template: "excel")]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetExcel()
    {
        byte[] file = new byte[1];
        if (file != null)
        {
            return File(fileContents: file, contentType:  MediaTypeNames.Application.Octet, fileDownloadName: "report.xlsx");
        }
        
        return NoContent();
    }
}