using CashFlow.Adapter.Exceptions;
using CashFlow.Communication.Requests.User;
using Microsoft.AspNetCore.Components;

namespace CashFlow.App.Pages.Profile.Components.UserProfile;

public partial class UserProfile
{
    [Parameter] public RequestUpdateUserJson? ProfileModel { get; set; }
    [Parameter] public EventCallback<RequestUpdateUserJson> UpdateProfileModel { get; set; }
    
    private bool _isUpdatingProfile;
    private bool _hasErrorFeedback;
    private List<string> _listFeedbacks = [];
    
    private async Task HandleUpdateProfileAsync()
    {
        _isUpdatingProfile = true;
        _listFeedbacks.Clear();

        try
        {
            await UserApiService.UpdateProfile(request: ProfileModel!);
            await UpdateProfileModel.InvokeAsync(arg: ProfileModel);
            _listFeedbacks = ["Informacoes atualizadas com sucesso."];
        }
        catch (ApiException exception) when (exception.ErrorMessages.Count > 0)
        {
            _listFeedbacks = exception.ErrorMessages.ToList();
            _hasErrorFeedback = true;
        }
        catch
        {
            _listFeedbacks = ["Nao foi possivel atualizar suas informacoes."];
            _hasErrorFeedback = true;
        }

        _isUpdatingProfile = false;
    }
}