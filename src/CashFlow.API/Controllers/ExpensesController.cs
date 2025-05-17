using CashFlow.Application.UseCases.Expenses.GetAll;
using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExpensesController : ControllerBase
{
  [HttpPost]
  [ProducesResponseType(typeof(ResponseRegisterExpenseJson), StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> Register([FromServices] IRegisterExpenseUseCase useCase, [FromBody] RequestRegisterExpenseJson request)
  {
    var response = await useCase.Execute(request);
    return Created(string.Empty, useCase);
  }

  [HttpGet]
  [ProducesResponseType(typeof(ResponseExpensesJson), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<IActionResult> GetAllExpenses([FromServices] IGetAllExpenseUseCase useCase)
  {
    var reponse = await useCase.Execute();
    if (reponse.Expenses.Count != 0)
    {
      return Ok(reponse);
    }

    return NoContent();
  }
}
