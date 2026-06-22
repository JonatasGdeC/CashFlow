using CashFlow.Communication.Enums;
using Microsoft.AspNetCore.Components;

namespace CashFlow.App.Pages.Categories.Components.CategoryHelper;

public partial class CategoryHelper
{
    [Parameter] public CategoryType SelectedType { get; set; }
    
    private static string GetCategoryTypeLabel(CategoryType type)
    {
        return type == CategoryType.Expense ? "Despesa" : "Renda";
    }
    
    private static string GetGoalMeaning(CategoryType type)
    {
        return type == CategoryType.Expense
            ? "Limite maximo que voce pretende gastar."
            : "Objetivo que voce pretende juntar.";
    }
}