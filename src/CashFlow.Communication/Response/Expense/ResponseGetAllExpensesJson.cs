namespace CashFlow.Communication.Response.Expense;

public class ResponseGetAllExpensesJson
{
    public List<ResponseExpenseShortJson> ListAllExpenses { get; set; } = [];
}