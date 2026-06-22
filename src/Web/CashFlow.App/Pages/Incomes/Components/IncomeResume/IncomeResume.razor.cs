
using CashFlow.Communication.Response.Income;
using Microsoft.AspNetCore.Components;

namespace CashFlow.App.Pages.Incomes.Components.IncomeResume;

public partial class IncomeResume
{
    [Parameter] public List<ResponseIncomeShortJson> Incomes { get; set; } = [];
    
    private decimal TotalAmount => Incomes.Sum(selector: income => income.Amount);
    private decimal AverageAmount => Incomes.Count == 0 ? 0 : TotalAmount / Incomes.Count;
}