using CashFlow.Communication.Requests;
using Microsoft.AspNetCore.WebUtilities;

namespace CashFlow.Adapter.Utils;

public static class RequestFilterJsonBuildQueryString
{
    public static string Execute(RequestFilterJson request)
    {
        Dictionary<string, string?> queryParams = new()
        {
            [key: "Date"] = request.Date.ToString(format: "yyyy-MM-dd"),
            [key: "CategoryId"] = request.CategoryId?.ToString(),
            [key: "Title"] = request.Title,
            [key: "Amount"] = request.Amount?.ToString(provider: System.Globalization.CultureInfo.InvariantCulture)
        };

        return QueryHelpers.AddQueryString(uri: "", queryString: queryParams).TrimStart(trimChar: '?');
    }
}