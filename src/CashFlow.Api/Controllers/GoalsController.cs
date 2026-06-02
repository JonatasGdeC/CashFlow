using CashFlow.Application.UsesCases.Goal.Delete;
using CashFlow.Application.UsesCases.Goal.GetByCategoryId;
using CashFlow.Application.UsesCases.Goal.GetById;
using CashFlow.Application.UsesCases.Goal.Register;
using CashFlow.Application.UsesCases.Goal.Update;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Requests.Goal;
using CashFlow.Communication.Response;
using CashFlow.Communication.Response.Goal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers;

[Route(template: "api/[controller]")]
[ApiController]
[Authorize]
public class GoalsController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(type: typeof(ResponseRegisterGoalJson), statusCode: StatusCodes.Status201Created)]
    [ProducesResponseType(type: typeof(ResponseErrorJson), statusCode: StatusCodes.Status400BadRequest)]
    [ProducesResponseType(type: typeof(ResponseErrorJson), statusCode: StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Register([FromServices] IRegisterGoalUseCase useCase, [FromBody] RequestRegisterGoalJson request)
    {
        ResponseRegisterGoalJson response = await useCase.Execute(request: request);
        return Created(uri: string.Empty, value: response);
    }

    [HttpGet]
    [Route(template: "{id}")]
    [ProducesResponseType(type: typeof(ResponseGoalJson), statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(type: typeof(ResponseErrorJson), statusCode: StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCategoryGoalById([FromServices] IGetGoalByIdUseCase useCase, [FromRoute] Guid id)
    {
        ResponseGoalJson response = await useCase.Execute(id: id);
        return Ok(value: response);
    }

    [HttpGet]
    [Route(template: "category/{categoryId}")]
    [ProducesResponseType(type: typeof(ResponseGoalJson), statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(type: typeof(ResponseErrorJson), statusCode: StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCategoryGoalByCategoryId([FromServices] IGetGoalByCategoryIdUseCase useCase, [FromRoute] Guid categoryId)
    {
        ResponseGoalJson response = await useCase.Execute(categoryId: categoryId);
        return Ok(value: response);
    }

    [HttpDelete]
    [Route(template: "{id}")]
    [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
    [ProducesResponseType(type: typeof(ResponseErrorJson), statusCode: StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromServices] IDeleteGoalUseCase useCase, [FromRoute] Guid id)
    {
        await useCase.Execute(id: id);
        return NoContent();
    }

    [HttpPut]
    [Route(template: "{id}")]
    [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
    [ProducesResponseType(type: typeof(ResponseErrorJson), statusCode: StatusCodes.Status400BadRequest)]
    [ProducesResponseType(type: typeof(ResponseErrorJson), statusCode: StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        [FromServices] IUpdateGoalUseCase useCase,
        [FromRoute] Guid id,
        [FromBody] RequestRegisterGoalJson request)
    {
        await useCase.Execute(id: id, request: request);
        return NoContent();
    }
}
