using CashFlow.Domain.Security.Tokens;

namespace CashFlow.Api.Token;

public class HttpContextTokenValue(IHttpContextAccessor accessor) : ITokenProvider
{
    public string TokenOnRequest()
    {
        string authorization = accessor.HttpContext!.Request.Headers.Authorization.ToString();
        return authorization.Replace(oldValue: "Bearer ", newValue: "");
    }
}