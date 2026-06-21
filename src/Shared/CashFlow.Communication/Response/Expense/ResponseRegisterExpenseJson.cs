namespace CashFlow.Communication.Response.Expense;

public record ResponseRegisterExpenseJson
{
    public string Title { get; init; } = string.Empty;
}