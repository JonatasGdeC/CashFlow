using CashFlow.Adapter.Exceptions;
using CashFlow.Communication.Requests.Income;
using CashFlow.Communication.Response.Income;
using Microsoft.AspNetCore.Components;

namespace CashFlow.App.Pages.Details;

public partial class IncomeDetails
{
    [Parameter] public Guid IncomeId { get; set; }

    private readonly RequestRegisterIncomeJson _editModel = new();
    private ResponseIncomeJson? _income;
    private bool _isLoading = true;
    private bool _isSaving;
    private bool _isDeleting;
    private bool _showEdit;
    private bool _showDelete;
    private List<string> _listFeedbacks = [];

    protected override async Task OnParametersSetAsync()
    {
        await LoadIncomeAsync();
    }

    private async Task LoadIncomeAsync()
    {
        _isLoading = true;
        _listFeedbacks.Clear();

        try
        {
            _income = await IncomeApiService.GetById(id: IncomeId);
            FillEditModel(income: _income);
        }
        catch (ApiException exception) when (exception.ErrorMessages.Count > 0)
        {
            _listFeedbacks = exception.ErrorMessages.ToList();
        }
        catch
        {
            _listFeedbacks = ["Nao foi possivel carregar a renda."];
        }

        _isLoading = false;
    }

    private void OpenEdit()
    {
        if (_income is not null)
        {
            FillEditModel(income: _income);
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

    private async Task HandleUpdateIncomeAsync()
    {
        _isSaving = true;
        _listFeedbacks.Clear();

        try
        {
            await IncomeApiService.Update(id: IncomeId, request: _editModel);
            _showEdit = false;
            await LoadIncomeAsync();
        }
        catch (ApiException exception) when (exception.ErrorMessages.Count > 0)
        {
            _listFeedbacks = exception.ErrorMessages.ToList();
        }
        catch
        {
            _listFeedbacks = ["Nao foi possivel atualizar a renda."];
        }

        _isSaving = false;
    }

    private async Task HandleDeleteIncomeAsync()
    {
        _isDeleting = true;
        _listFeedbacks.Clear();

        try
        {
            await IncomeApiService.Delete(id: IncomeId);
            NavigationManager.NavigateTo(uri: "incomes");
        }
        catch (ApiException exception) when (exception.ErrorMessages.Count > 0)
        {
            _listFeedbacks = exception.ErrorMessages.ToList();
            _isDeleting = false;
        }
        catch
        {
            _listFeedbacks = ["Nao foi possivel excluir a renda."];
            _isDeleting = false;
        }
    }

    private void FillEditModel(ResponseIncomeJson income)
    {
        _editModel.Title = income.Title;
        _editModel.Description = income.Description;
        _editModel.Date = income.Date;
        _editModel.Amount = income.Amount;
    }

    private static string GetInitial(string value)
    {
        return string.IsNullOrWhiteSpace(value: value)
            ? "?"
            : value.Trim()[index: 0].ToString().ToUpperInvariant();
    }
}
