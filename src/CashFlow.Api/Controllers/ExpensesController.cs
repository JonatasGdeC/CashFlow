using System;
using System.Threading.Tasks;
using CashFlow.Application.UsesCases.Expense.Delete;
using CashFlow.Application.UsesCases.Expense.GetAll;
using CashFlow.Application.UsesCases.Expense.GetById;
using CashFlow.Application.UsesCases.Expense.GetDashboard;
using CashFlow.Application.UsesCases.Expense.Register;
using CashFlow.Application.UsesCases.Expense.Update;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers;

[Route(template: "api/[controller]")]
[ApiController]
[Authorize]
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
    
    [HttpGet(template: "dashboard")]
    [ProducesResponseType(type: typeof(ResponseDashboardJson), statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetDashboard([FromServices] IGetExpenseDashboard useCase)
    {
        ResponseDashboardJson response = await useCase.Execute();
        return Ok(value: response);
    }

    [HttpGet]
    [Route(template: "{id}")]
    [ProducesResponseType(type: typeof(ResponseExpenseJson), statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(type: typeof(ResponseErrorJson), statusCode: StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetExpenseById([FromServices] IGetByIdExpenseUseCase useCase, [FromRoute] Guid id)
    {
       ResponseExpenseJson response = await useCase.Execute(id: id);
       return Ok(value: response);
    }

    [HttpDelete]
    [Route(template: "{id}")]
    [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
    [ProducesResponseType(type: typeof(ResponseErrorJson), statusCode: StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromServices] IDeleteExpenseUseCase useCase, [FromRoute] Guid id)
    {
        await useCase.Execute(id: id);
        return NoContent();
    }

    [HttpPut]
    [Route(template: "{id}")]
    [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
    [ProducesResponseType(type: typeof(ResponseErrorJson), statusCode: StatusCodes.Status400BadRequest)]
    [ProducesResponseType(type: typeof(ResponseErrorJson), statusCode: StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromServices] IUpdateExpenseUseCase useCase, [FromRoute] Guid id, [FromBody] RequestRegisterExpenseJson request)
    {
        await useCase.Execute(id: id, request: request);
        return NoContent();
    }
}
