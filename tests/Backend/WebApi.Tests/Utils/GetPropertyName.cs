namespace WebApi.Tests.Utils;

public static class GetPropertyName
{
    public static string HandlePropertyName(this string propertyName)
    {
        return char.ToLowerInvariant(c: propertyName[index: 0]) + propertyName[1..];
    }
}