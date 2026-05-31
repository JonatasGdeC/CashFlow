namespace CashFlow.Communication.Response.Expense;

public record ResponseGetAllExpensesJson
{
    public List<ResponseExpenseShortJson> ListAllExpenses { get; init; } = [];
}