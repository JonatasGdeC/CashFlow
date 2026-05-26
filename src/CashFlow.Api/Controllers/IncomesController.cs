using CashFlow.Application.UsesCases.Income.Delete;
using CashFlow.Application.UsesCases.Income.GetAll;
using CashFlow.Application.UsesCases.Income.GetById;
using CashFlow.Application.UsesCases.Income.GetDashboard;
using CashFlow.Application.UsesCases.Income.Register;
using CashFlow.Application.UsesCases.Income.Update;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers;

[Route(template: "api/[controller]")]
[ApiController]
[Authorize]
public class IncomesController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(type: typeof(ResponseRegisterIncomeJson), statusCode: StatusCodes.Status201Created)]
    [ProducesResponseType(type: typeof(ResponseErrorJson), statusCode: StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromServices] IRegisterIncomeUseCase useCase, [FromBody] RequestRegisterIncomeJson request)
    {
        ResponseRegisterIncomeJson response = await useCase.Execute(request: request);
        return Created(uri: string.Empty, value: response);
    }

    [HttpGet]
    [ProducesResponseType(type: typeof(ResponseGetAllIncomesJson), statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllIncomes([FromServices] IGetAllIncomeUseCase useCase)
    {
        ResponseGetAllIncomesJson response = await useCase.Execute();
        if (response.ListAllIncomes.Count > 0)
        {
            return Ok(value: response);
        }

        return NoContent();
    }

    [HttpGet(template: "dashboard")]
    [ProducesResponseType(type: typeof(ResponseDashboardJson), statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetDashboard([FromServices] IGetIncomeDashboard useCase)
    {
        ResponseDashboardJson response = await useCase.Execute();
        return Ok(value: response);
    }

    [HttpGet]
    [Route(template: "{id}")]
    [ProducesResponseType(type: typeof(ResponseIncomeJson), statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(type: typeof(ResponseErrorJson), statusCode: StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetIncomeById([FromServices] IGetByIdIncomeUseCase useCase, [FromRoute] Guid id)
    {
        ResponseIncomeJson response = await useCase.Execute(id: id);
        return Ok(value: response);
    }

    [HttpDelete]
    [Route(template: "{id}")]
    [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
    [ProducesResponseType(type: typeof(ResponseErrorJson), statusCode: StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromServices] IDeleteIncomeUseCase useCase, [FromRoute] Guid id)
    {
        await useCase.Execute(id: id);
        return NoContent();
    }

    [HttpPut]
    [Route(template: "{id}")]
    [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
    [ProducesResponseType(type: typeof(ResponseErrorJson), statusCode: StatusCodes.Status400BadRequest)]
    [ProducesResponseType(type: typeof(ResponseErrorJson), statusCode: StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromServices] IUpdateIncomeUseCase useCase, [FromRoute] Guid id, [FromBody] RequestRegisterIncomeJson request)
    {
        await useCase.Execute(id: id, request: request);
        return NoContent();
    }
}
