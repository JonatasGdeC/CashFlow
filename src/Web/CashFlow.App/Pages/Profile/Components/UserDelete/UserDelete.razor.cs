using CashFlow.Adapter.Exceptions;
using CashFlow.Communication.Requests.User;
using Microsoft.AspNetCore.Components;

namespace CashFlow.App.Pages.Profile.Components.UserDelete;

public partial class UserDelete
{
    [Parameter] public RequestUpdateUserJson? ProfileModel { get; set; }
    
    private bool _isDeletingAccount;
    private bool _showDeleteConfirmation;
    private bool _hasErrorFeedback;
    private List<string> _listFeedbacks = [];
    
    private void OpenDeleteConfirmation()
    {
        _listFeedbacks.Clear();
        _showDeleteConfirmation = true;
    }

    private void CloseDeleteConfirmation()
    {
        if (_isDeletingAccount)
        {
            return;
        }

        _showDeleteConfirmation = false;
    }
    
    private async Task HandleDeleteAccountAsync()
    {
        _isDeletingAccount = true;
        _listFeedbacks.Clear();
        
        try
        {
            await UserApiService.Delete();
            await CookieAuthenticationStateProvider.RemoveTokenAsync();
        }
        catch (ApiException exception) when (exception.ErrorMessages.Count > 0)
        {
            _listFeedbacks = exception.ErrorMessages.ToList();
            _isDeletingAccount = false;
            _hasErrorFeedback = true;
        }
        catch
        {
            _listFeedbacks = ["Nao foi possivel excluir sua conta."];
            _hasErrorFeedback = true;
            _isDeletingAccount = false;
        }
    }
}