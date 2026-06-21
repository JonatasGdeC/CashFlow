namespace CashFlow.Communication.Response.Income;

public record ResponseGetAllIncomesJson
{
    public List<ResponseIncomeShortJson> ListAllIncomes { get; init; } = [];
}
