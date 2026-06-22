using CashFlow.Adapter.Exceptions;
using CashFlow.Communication.Requests.User;
using CashFlow.Communication.Response.User;

namespace CashFlow.App.Pages.Profile.Main;

public partial class Profile
{
    private RequestUpdateUserJson _profileModel = new()
    {
        Name = string.Empty,
        Email = string.Empty
    };

    private bool _isLoading = true;
    private List<string> _listFeedbacks = [];

    protected override async Task OnInitializedAsync()
    {
        await LoadProfileAsync();
    }
    
    private async Task LoadProfileAsync()
    {
        _isLoading = true;

        try
        {
            ResponseUserProfileJson profile = await UserApiService.GetProfile();
            _profileModel.Name = profile.Name;
            _profileModel.Email = profile.Email;
        }
        catch (ApiException exception) when (exception.ErrorMessages.Count > 0)
        {
            _listFeedbacks = exception.ErrorMessages.ToList();
        }
        catch
        {
            _listFeedbacks = ["Nao foi possivel carregar os dados do perfil."];
        }

        _isLoading = false;
    }
}
