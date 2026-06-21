using CashFlow.Application.UsesCases.User.Delete;
using CashFlow.Application.UsesCases.User.Get;
using CashFlow.Application.UsesCases.User.Register;
using CashFlow.Application.UsesCases.User.Update;
using CashFlow.Application.UsesCases.User.UpdatePassword;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Requests.User;
using CashFlow.Communication.Response;
using CashFlow.Communication.Response.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers;

[Route(template: "api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(type: typeof(ResponseRegisterUserJson), statusCode: StatusCodes.Status201Created)]
    [ProducesResponseType(type: typeof(ResponseErrorJson), statusCode: StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromServices] IRegisterUserUseCase useCase, [FromBody] RequestRegisterUserJson request)
    {
        ResponseRegisterUserJson response = await useCase.Execute(request: request);
        return Created(uri: string.Empty, value: response);
    }
    
    [HttpGet]
    [Authorize]
    [ProducesResponseType(type: typeof(ResponseUserProfileJson), statusCode: StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProfile([FromServices] IGetUserProfileUseCase useCase)
    {
        ResponseUserProfileJson response = await useCase.Execute();
        return Ok(value: response);
    }

    [HttpPut]
    [Authorize]
    [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
    [ProducesResponseType(type: typeof(ResponseErrorJson), statusCode: StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateProfile([FromServices] IUpdateUserUseCase useCase, [FromBody] RequestUpdateUserJson request)
    {
        await useCase.Execute(request: request);
        return NoContent();
    }

    [HttpPut(template: "change-password")]
    [Authorize]
    [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
    [ProducesResponseType(type: typeof(ResponseErrorJson), statusCode: StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ChangePassword([FromServices] IChangePasswordUseCase useCase, [FromBody] RequestChangePasswordJson request)
    {
        await useCase.Execute(request: request);
        return NoContent();
    }
    
    [HttpDelete]
    [Authorize]
    [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteProfile([FromServices] IDeleteUserAccountUseCase useCase)
    {
        await useCase.Execute();
        return NoContent();
    }
}
