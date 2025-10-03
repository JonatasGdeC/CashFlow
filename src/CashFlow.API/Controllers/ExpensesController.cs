using CashFlow.Application.UseCases.Expenses.Delete;
using CashFlow.Application.UseCases.Expenses.GetAll;
using CashFlow.Application.UseCases.Expenses.GetById;
using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Application.UseCases.Expenses.Update;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.API.Controllers;

[Route(template: "api/[controller]")]
[ApiController]
public class ExpensesController : ControllerBase
{
  [HttpPost]
  [ProducesResponseType(type: typeof(ResponseRegisterExpenseJson), statusCode: StatusCodes.Status201Created)]
  [ProducesResponseType(type: typeof(ResponseErrorJson), statusCode: StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> Register([FromServices] IRegisterExpenseUseCase useCase, [FromBody] RequestExpenseJson request)
  {
    ResponseRegisterExpenseJson response = await useCase.Execute(request: request);
    return Created(uri: string.Empty, value: useCase);
  }

  [HttpGet]
  [ProducesResponseType(type: typeof(ResponseExpensesJson), statusCode: StatusCodes.Status200OK)]
  [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
  public async Task<IActionResult> GetAllExpenses([FromServices] IGetAllExpenseUseCase useCase)
  {
    ResponseExpensesJson reponse = await useCase.Execute();
    if (reponse.Expenses.Count != 0)
    {
      return Ok(value: reponse);
    }

    return NoContent();
  }

  [HttpGet]
  [Route(template: "{id}")]
  [ProducesResponseType(type: typeof(ResponseExpenseJson), statusCode: StatusCodes.Status200OK)]
  [ProducesResponseType(type: typeof(ResponseErrorJson), statusCode: StatusCodes.Status404NotFound)]
  public async Task<IActionResult> GetById([FromServices] IGetExpenseByIdUseCase useCase, [FromRoute] long id)
  {
    ResponseExpenseJson response = await useCase.Execute(id: id);
    return Ok(value: response);
  }

  [HttpDelete]
  [Route(template: "{id}")]
  [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
  [ProducesResponseType(type: typeof(ResponseErrorJson), statusCode: StatusCodes.Status404NotFound)]
  public async Task<IActionResult> Delete([FromServices] IDeleteExpenseUseCase useCase, [FromRoute] long id)
  {
    await useCase.Execute(id: id);
    return NoContent();
  }

  [HttpPut]
  [Route(template: "{id}")]
  [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
  [ProducesResponseType(type: typeof(ResponseErrorJson), statusCode: StatusCodes.Status400BadRequest)]
  [ProducesResponseType(type: typeof(ResponseErrorJson), statusCode: StatusCodes.Status404NotFound)]
  public async Task<IActionResult> Update([FromServices] IUpdateExpenseUseCase useCase, [FromRoute] long id, [FromBody] RequestExpenseJson request)
  {
    await useCase.Execute(id: id, request: request);
    return NoContent();
  }
}
