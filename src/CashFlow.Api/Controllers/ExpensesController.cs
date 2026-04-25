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
    public IActionResult Register([FromBody] RequestRegisterExpenseJson request)
    {
        RegisterExpenseUseCase useCase = new();
        ResponseRegisterExpenseJson response = useCase.Execute(request: request);
        return Created(uri: string.Empty, value: response);
    }
}