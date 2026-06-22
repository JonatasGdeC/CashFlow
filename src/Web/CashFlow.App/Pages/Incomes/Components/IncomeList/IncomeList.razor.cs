
using CashFlow.Communication.Response.Income;
using Microsoft.AspNetCore.Components;

namespace CashFlow.App.Pages.Incomes.Components.IncomeList;

public partial class IncomeList
{
    [Parameter] public List<ResponseIncomeShortJson> Incomes { get; set; } = [];
}