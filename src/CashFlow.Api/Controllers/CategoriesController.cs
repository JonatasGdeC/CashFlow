using CashFlow.Application.UsesCases.Category.Delete;
using CashFlow.Application.UsesCases.Category.GetAll;
using CashFlow.Application.UsesCases.Category.GetById;
using CashFlow.Application.UsesCases.Category.Register;
using CashFlow.Application.UsesCases.Category.Update;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Requests.Category;
using CashFlow.Communication.Response;
using CashFlow.Communication.Response.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers;

[Route(template: "api/[controller]")]
[ApiController]
[Authorize]
public class CategoriesController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(type: typeof(ResponseRegisterCategoryJson), statusCode: StatusCodes.Status201Created)]
    [ProducesResponseType(type: typeof(ResponseErrorJson), statusCode: StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromServices] IRegisterCategoryUseCase useCase, [FromBody] RequestRegisterCategoryJson request)
    {
        ResponseRegisterCategoryJson response = await useCase.Execute(request: request);
        return Created(uri: string.Empty, value: response);
    }

    [HttpGet]
    [ProducesResponseType(type: typeof(ResponseGetAllCategoriesJson), statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllCategories([FromServices] IGetAllCategoryUseCase useCase, [FromQuery] CategoryType categoryType)
    {
        ResponseGetAllCategoriesJson response = await useCase.Execute(categoryType: categoryType);
        if (response.ListAllCategories.Count > 0)
        {
            return Ok(value: response);
        }

        return NoContent();
    }

    [HttpGet]
    [Route(template: "{id}")]
    [ProducesResponseType(type: typeof(ResponseCategoryJson), statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(type: typeof(ResponseErrorJson), statusCode: StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCategoryById([FromServices] IGetByIdCategoryUseCase useCase, [FromRoute] Guid id)
    {
        ResponseCategoryJson response = await useCase.Execute(id: id);
        return Ok(value: response);
    }

    [HttpDelete]
    [Route(template: "{id}")]
    [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
    [ProducesResponseType(type: typeof(ResponseErrorJson), statusCode: StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromServices] IDeleteCategoryUseCase useCase, [FromRoute] Guid id)
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
        [FromServices] IUpdateCategoryUseCase useCase,
        [FromRoute] Guid id,
        [FromBody] RequestRegisterCategoryJson request)
    {
        await useCase.Execute(id: id, request: request);
        return NoContent();
    }
}
