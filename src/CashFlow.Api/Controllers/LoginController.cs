using CashFlow.Application.UsesCases.User.Login;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Response;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers;

[Route(template: "api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(type: typeof(ResponseRegisterUserJson), statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(type: typeof(ResponseErrorJson), statusCode: StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromServices] ILoginUseCase useCase, [FromBody] RequestLoginJson request)
    {
        ResponseRegisterUserJson response = await useCase.Execute(request: request);
        return Ok(value: response);
    }
}