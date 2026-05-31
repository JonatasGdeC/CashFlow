using CashFlow.Application.UsesCases.CategoryGoal.Delete;
using CashFlow.Application.UsesCases.CategoryGoal.GetByCategoryId;
using CashFlow.Application.UsesCases.CategoryGoal.GetById;
using CashFlow.Application.UsesCases.CategoryGoal.Register;
using CashFlow.Application.UsesCases.CategoryGoal.Update;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Requests.CategoryGoal;
using CashFlow.Communication.Response;
using CashFlow.Communication.Response.CategoryGoal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers;

[Route(template: "api/[controller]")]
[ApiController]
[Authorize]
public class CategoriesGoalsController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(type: typeof(ResponseRegisterCategoryGoalJson), statusCode: StatusCodes.Status201Created)]
    [ProducesResponseType(type: typeof(ResponseErrorJson), statusCode: StatusCodes.Status400BadRequest)]
    [ProducesResponseType(type: typeof(ResponseErrorJson), statusCode: StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Register([FromServices] IRegisterCategoryGoalUseCase useCase, [FromBody] RequestRegisterCategoryGoalJson request)
    {
        ResponseRegisterCategoryGoalJson response = await useCase.Execute(request: request);
        return Created(uri: string.Empty, value: response);
    }

    [HttpGet]
    [Route(template: "{id}")]
    [ProducesResponseType(type: typeof(ResponseCategoryGoalJson), statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(type: typeof(ResponseErrorJson), statusCode: StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCategoryGoalById([FromServices] IGetByIdCategoryGoalUseCase useCase, [FromRoute] Guid id)
    {
        ResponseCategoryGoalJson response = await useCase.Execute(id: id);
        return Ok(value: response);
    }

    [HttpGet]
    [Route(template: "category/{categoryId}")]
    [ProducesResponseType(type: typeof(ResponseCategoryGoalJson), statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(type: typeof(ResponseErrorJson), statusCode: StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCategoryGoalByCategoryId([FromServices] IGetCategoryGoalByCategoryIdUseCase useCase, [FromRoute] Guid categoryId)
    {
        ResponseCategoryGoalJson response = await useCase.Execute(categoryId: categoryId);
        return Ok(value: response);
    }

    [HttpDelete]
    [Route(template: "{id}")]
    [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
    [ProducesResponseType(type: typeof(ResponseErrorJson), statusCode: StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromServices] IDeleteCategoryGoalUseCase useCase, [FromRoute] Guid id)
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
        [FromServices] IUpdateCategoryGoalUseCase useCase,
        [FromRoute] Guid id,
        [FromBody] RequestRegisterCategoryGoalJson request)
    {
        await useCase.Execute(id: id, request: request);
        return NoContent();
    }
}
