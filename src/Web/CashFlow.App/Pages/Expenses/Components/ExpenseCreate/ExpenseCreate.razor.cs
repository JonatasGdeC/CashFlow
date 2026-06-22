using CashFlow.Adapter.Exceptions;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests.Expense;
using CashFlow.Communication.Response.Category;
using Microsoft.AspNetCore.Components;

namespace CashFlow.App.Pages.Expenses.Components.ExpenseCreate;

public partial class ExpenseCreate
{
    [Parameter] public EventCallback OnExpenseCreated { get; set; }
    [Parameter] public EventCallback OnCancel { get; set; }

    private readonly RequestRegisterExpenseJson _expenseModel = new()
    {
        Date = DateTime.Today,
        PaymentType = PaymentType.Cash
    };

    private bool _isSubmitting;
    private List<ResponseCategoryShortJson> _expenseCategories = [];
    private List<string> _listFeedbacks = [];

    protected override async Task OnInitializedAsync()
    {
        try
        {
            ResponseGetAllCategoriesJson? response = await CategoryApiService.GetAll(categoryType: CategoryType.Expense);
            _expenseCategories = response?.ListAllCategories ?? [];
        }
        catch
        {
            _expenseCategories = [];
        }
    }

    private async Task HandleCreateExpenseAsync()
    {
        _isSubmitting = true;
        _listFeedbacks.Clear();

        try
        {
            await ExpenseApiService.Register(request: _expenseModel);
            await OnExpenseCreated.InvokeAsync();
        }
        catch (ApiException exception) when (exception.ErrorMessages.Count > 0)
        {
            _listFeedbacks = exception.ErrorMessages.ToList();
        }
        catch
        {
            _listFeedbacks = ["Nao foi possivel registrar a despesa."];
        }

        _isSubmitting = false;
    }

    private async Task HandleCancelAsync()
    {
        await OnCancel.InvokeAsync();
    }

    private static string GetPaymentTypeDescription(PaymentType paymentType)
    {
        return paymentType switch
        {
            PaymentType.Cash => "Dinheiro",
            PaymentType.CreditCard => "Cartao de credito",
            PaymentType.DebitCard => "Cartao de debito",
            PaymentType.Electronic => "Eletronico",
            _ => paymentType.ToString()
        };
    }
}
