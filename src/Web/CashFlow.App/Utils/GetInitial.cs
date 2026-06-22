namespace CashFlow.App.Utils;

public static class GetInitial
{
    public static string Execute(string value)
    {
        return string.IsNullOrWhiteSpace(value: value)
            ? "?"
            : value.Trim()[index: 0].ToString().ToUpperInvariant();
    }
}