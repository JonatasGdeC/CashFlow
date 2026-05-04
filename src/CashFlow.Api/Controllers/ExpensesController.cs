using CashFlow.Application.UsesCases.Expense.GetAll;
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
    public async Task<IActionResult> Register([FromServices] IRegisterExpenseUseCase useCase, [FromBody] RequestRegisterExpenseJson request)
    {
        ResponseRegisterExpenseJson response = await useCase.Execute(request: request);
        return Created(uri: string.Empty, value: response);
    }

    [HttpGet]
    [ProducesResponseType(type: typeof(ResponseGetAllExpensesJson), statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllExpenses([FromServices] IGetAllExpenseUseCase useCase)
    {
        ResponseGetAllExpensesJson response = await useCase.Execute();
        if (response.ListAllExpenses.Count > 0)
        {
            return Ok(value: response);
        }

        return NoContent();
    }
}