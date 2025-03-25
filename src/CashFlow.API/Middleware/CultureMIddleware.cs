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
    var supportedCultures = CultureInfo.GetCultures(CultureTypes.AllCultures).ToList();
    var requestedCulture = context.Request.Headers.AcceptLanguage.FirstOrDefault();
    var cultureInfo = new CultureInfo("en");

    if (!string.IsNullOrWhiteSpace(requestedCulture) && supportedCultures.Exists(language => language.Name.Equals(requestedCulture)))
    {
      cultureInfo = new CultureInfo(requestedCulture);
    }

    CultureInfo.CurrentCulture = cultureInfo;
    CultureInfo.CurrentUICulture = cultureInfo;

    await _next(context);
  }
}
