using CashFlow.Application.UsesCases.Expense.Register;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Response;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers;

[Route(template: "api/[controller]")]
[ApiController]
public class ExpensesController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(type: typeof(ResponseRegisterExpenseJson), statusCode: StatusCodes.Status201Created)]
    [ProducesResponseType(type: typeof(ResponseErrorJson), statusCode: StatusCodes.Status400BadRequest)]
    public IActionResult Register([FromServices] IRegisterExpenseUseCase useCase, [FromBody] RequestRegisterExpenseJson request)
    {
        ResponseRegisterExpenseJson response = useCase.Execute(request: request);
        return Created(uri: string.Empty, value: response);
    }
}