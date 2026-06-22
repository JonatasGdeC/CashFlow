using CashFlow.Communication.Response.Expense;
using Microsoft.AspNetCore.Components;

namespace CashFlow.App.Pages.Expenses.Components.ExpenseList;

public partial class ExpenseList
{
    [Parameter] public IReadOnlyList<ResponseExpenseShortJson> Expenses { get; set; } = [];
}
