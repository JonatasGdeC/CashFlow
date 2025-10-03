using CashFlow.Communication.Responses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CashFlow.API.Filters;

public class ExceptionFilter : IExceptionFilter
{
  public void OnException(ExceptionContext context)
  {
    if (context.Exception is CashFlowException)
    {
      HandleProjectException(context: context);
    }
    else
    {
      ThrowUnknowError(context: context);
    }
  }

  private void HandleProjectException(ExceptionContext context)
  {
    CashFlowException? cashFlowException = context.Exception as CashFlowException;
    ResponseErrorJson errorResponse = new ResponseErrorJson(errorMessages: cashFlowException!.GetErrors());

    context.HttpContext.Response.StatusCode = cashFlowException.StatusCode;
    context.Result = new ObjectResult(value: errorResponse);
  }

  private void ThrowUnknowError(ExceptionContext context)
  {
    ResponseErrorJson errorResponse = new ResponseErrorJson(errorMessage: ResourcesErrorMessages.UNKOWN_ERROR);
    context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
    context.Result = new ObjectResult(value: errorResponse);
  }
}
