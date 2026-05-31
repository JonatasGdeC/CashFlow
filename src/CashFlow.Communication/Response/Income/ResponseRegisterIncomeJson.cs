namespace CashFlow.Communication.Response.Income;

public record ResponseRegisterIncomeJson
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
}
