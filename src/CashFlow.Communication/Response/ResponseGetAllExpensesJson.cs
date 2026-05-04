namespace CashFlow.Communication.Response;

public class ResponseGetAllExpensesJson
{
    public List<ResponseExpenseShortJson> ListAllExpenses { get; set; } = [];
}