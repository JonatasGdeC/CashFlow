using CashFlow.Communication.Response;
using CashFlow.Exception.ExceptionBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CashFlow.Api.Filters;

public class ExceptionFilter: IExceptionFilter
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
        CashFlowException? kanbanException = context.Exception as CashFlowException;
        ResponseErrorJson errorResponse = new(errorMessages: kanbanException!.GetErrors());

        context.HttpContext.Response.StatusCode = kanbanException.StatusCode;
        context.Result = new ObjectResult(value: errorResponse);
    }

    private void ThrowUnknowError(ExceptionContext context)
    {
        ResponseErrorJson errorResponse = new(errorMessage: "Unknow error");
        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(value: errorResponse);
    }
}