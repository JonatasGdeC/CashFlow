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
    List<CultureInfo> supportedCultures = CultureInfo.GetCultures(types: CultureTypes.AllCultures).ToList();
    string? requestedCulture = context.Request.Headers.AcceptLanguage.FirstOrDefault();
    CultureInfo cultureInfo = new CultureInfo(name: "en");

    if (!string.IsNullOrWhiteSpace(value: requestedCulture) && supportedCultures.Exists(match: language => language.Name.Equals(value: requestedCulture)))
    {
      cultureInfo = new CultureInfo(name: requestedCulture);
    }

    CultureInfo.CurrentCulture = cultureInfo;
    CultureInfo.CurrentUICulture = cultureInfo;

    await _next(context: context);
  }
}
