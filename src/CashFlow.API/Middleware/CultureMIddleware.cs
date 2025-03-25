using System.Globalization;

namespace CashFlow.API.Middleware;

public class CultureMIddleware
{
  private readonly RequestDelegate _next;

  public CultureMIddleware(RequestDelegate next)
  {
    _next = next;
  }

  public async Task Invoke(HttpContext context)
  {
     var culture = context.Request.Headers.AcceptLanguage.FirstOrDefault();
     var cultureInfo = new CultureInfo("en");

     if (!string.IsNullOrWhiteSpace(culture))
     {
       cultureInfo = new CultureInfo(culture);
     }

     CultureInfo.CurrentCulture = cultureInfo;
     CultureInfo.CurrentUICulture = cultureInfo;

     await _next(context);
  }
}
