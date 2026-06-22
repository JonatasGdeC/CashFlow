using CashFlow.Adapter.Exceptions;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests.Expense;
using CashFlow.Communication.Response.Category;
using CashFlow.Communication.Response.Expense;
using Microsoft.AspNetCore.Components;

namespace CashFlow.App.Pages.Details;

public partial class ExpenseDetails
{
    [Parameter] public Guid ExpenseId { get; set; }

    private readonly RequestRegisterExpenseJson _editModel = new();
    private ResponseExpenseJson? _expense;
    private List<ResponseCategoryShortJson> _expenseCategories = [];
    private bool _isLoading = true;
    private bool _isSaving;
    private bool _isDeleting;
    private bool _showEdit;
    private bool _showDelete;
    private List<string> _listFeedbacks = [];

    protected override async Task OnParametersSetAsync()
    {
        await LoadExpenseAsync();
    }

    private async Task LoadExpenseAsync()
    {
        _isLoading = true;
        _listFeedbacks.Clear();

        try
        {
            ResponseGetAllCategoriesJson? categoriesResponse = await CategoryApiService.GetAll(categoryType: CategoryType.Expense);
            _expenseCategories = categoriesResponse?.ListAllCategories ?? [];
            _expense = await ExpenseApiService.GetById(id: ExpenseId);
            FillEditModel(expense: _expense);
        }
        catch (ApiException exception) when (exception.ErrorMessages.Count > 0)
        {
            _listFeedbacks = exception.ErrorMessages.ToList();
        }
        catch
        {
            _listFeedbacks = ["Nao foi possivel carregar a despesa."];
        }

        _isLoading = false;
    }

    private void OpenEdit()
    {
        if (_expense is not null)
        {
            FillEditModel(expense: _expense);
        }

        _listFeedbacks.Clear();
        _showEdit = true;
    }

    private void CloseEdit()
    {
        if (!_isSaving)
        {
            _showEdit = false;
        }
    }

    private void OpenDelete()
    {
        _listFeedbacks.Clear();
        _showDelete = true;
    }

    private void CloseDelete()
    {
        if (!_isDeleting)
        {
            _showDelete = false;
        }
    }

    private async Task HandleUpdateExpenseAsync()
    {
        _isSaving = true;
        _listFeedbacks.Clear();

        try
        {
            await ExpenseApiService.Update(id: ExpenseId, request: _editModel);
            _showEdit = false;
            await LoadExpenseAsync();
        }
        catch (ApiException exception) when (exception.ErrorMessages.Count > 0)
        {
            _listFeedbacks = exception.ErrorMessages.ToList();
        }
        catch
        {
            _listFeedbacks = ["Nao foi possivel atualizar a despesa."];
        }

        _isSaving = false;
    }

    private async Task HandleDeleteExpenseAsync()
    {
        _isDeleting = true;
        _listFeedbacks.Clear();

        try
        {
            await ExpenseApiService.Delete(id: ExpenseId);
            NavigationManager.NavigateTo(uri: "expenses");
        }
        catch (ApiException exception) when (exception.ErrorMessages.Count > 0)
        {
            _listFeedbacks = exception.ErrorMessages.ToList();
            _isDeleting = false;
        }
        catch
        {
            _listFeedbacks = ["Nao foi possivel excluir a despesa."];
            _isDeleting = false;
        }
    }

    private void FillEditModel(ResponseExpenseJson expense)
    {
        _editModel.Title = expense.Title;
        _editModel.Description = expense.Description;
        _editModel.Date = expense.Date;
        _editModel.Amount = expense.Amount;
        _editModel.PaymentType = expense.PaymentType;
        _editModel.CategoryId = expense.CategoryId;
    }

    private static string GetExpenseInitial(string title)
    {
        return string.IsNullOrWhiteSpace(value: title)
            ? "?"
            : title.Trim()[index: 0].ToString().ToUpperInvariant();
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
