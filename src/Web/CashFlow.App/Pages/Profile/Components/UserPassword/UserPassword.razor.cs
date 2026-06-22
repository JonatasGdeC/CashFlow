using CashFlow.Adapter.Exceptions;
using CashFlow.Communication.Requests.User;

namespace CashFlow.App.Pages.Profile.Components.UserPassword;

public partial class UserPassword
{
    private readonly RequestChangePasswordJson _passwordModel = new()
    {
        OldPassword = string.Empty,
        NewPassword = string.Empty
    };
    
    private bool _isChangingPassword;
    private bool _hasErrorFeedback;
    private List<string> _listFeedbacks = [];
    
    private async Task HandleChangePasswordAsync()
    {
        _isChangingPassword = true;
        _listFeedbacks.Clear();

        try
        {
            await UserApiService.ChangePassword(request: _passwordModel);
            _passwordModel.OldPassword = string.Empty;
            _passwordModel.NewPassword = string.Empty;
            _listFeedbacks = ["Senha atualizada com sucesso."];
        }
        catch (ApiException exception) when (exception.ErrorMessages.Count > 0)
        {
            _listFeedbacks = exception.ErrorMessages.ToList();
            _hasErrorFeedback = true;
        }
        catch
        {
            _listFeedbacks = ["Nao foi possivel atualizar sua senha."];
            _hasErrorFeedback = true;
        }

        _isChangingPassword = false;
    }
}