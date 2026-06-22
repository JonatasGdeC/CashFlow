using CashFlow.Communication.Response.Expense;
using Microsoft.AspNetCore.Components;

namespace CashFlow.App.Pages.Expenses.Components.ExpenseResume;

public partial class ExpenseResume
{
    [Parameter] public List<ResponseExpenseShortJson> Expenses { get; set; } = [];
    
    private decimal TotalAmount => Expenses.Sum(selector: expense => expense.Amount);
}