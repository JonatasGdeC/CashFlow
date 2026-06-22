using CashFlow.Adapter.Exceptions;
using CashFlow.Communication.Requests.User;
using CashFlow.Communication.Response.User;

namespace CashFlow.App.Pages.Login;

public partial class Login
{
    private readonly RequestRegisterUserJson _registerModel = new()
    {
        Name = string.Empty,
        Email = string.Empty,
        Password = string.Empty
    };

    private readonly RequestLoginJson _loginRequest = new()
    {
        Email = string.Empty,
        Password = string.Empty
    };

    private bool _isRegistering;
    private bool _isSubmitting;
    private List<string> _listFeedbacks = [];

    private string FormTitle => _isRegistering ? "Criar conta" : "Entrar";
    private string FormSubtitle => _isRegistering
        ? "Informe nome, email e senha para registrar um novo acesso."
        : "Use seu email e senha para acessar as despesas registradas.";

    private void ShowLogin()
    {
        _listFeedbacks.Clear();
        _isRegistering = false;
    }

    private void ShowRegister()
    {
        _listFeedbacks.Clear();
        _isRegistering = true;
    }

    private async Task HandleLoginAsync()
    {
        _listFeedbacks.Clear();
        _isSubmitting = true;

        try
        {
            ResponseRegisterUserJson response =await LoginApiService.Login(request: _loginRequest);
            await CookieAuthenticationStateProvider.SetTokenAsync(token: response.Token);
        }
        catch (ApiException exception) when (exception.ErrorMessages.Count > 0)
        {
            _listFeedbacks = exception.ErrorMessages.ToList();
        }
        catch
        {
            _listFeedbacks = ["Nao foi possivel realizar o login na sua conta. Confira os dados e tente novamente."];
        }
        
        _isSubmitting = false;
    }
    
    private async Task HandleRegisterAsync()
    {
        _listFeedbacks.Clear();
        _isSubmitting = true;

        try
        {
            ResponseRegisterUserJson response = await UserApiService.Register(request: _registerModel);
            await CookieAuthenticationStateProvider.SetTokenAsync(token: response.Token);
        }
        catch (ApiException exception) when (exception.ErrorMessages.Count > 0)
        {
            _listFeedbacks = exception.ErrorMessages.ToList();
        }
        catch
        {
            _listFeedbacks = ["Nao foi possivel criar a conta. Confira os dados e tente novamente."];
        }
        
        _isSubmitting = false;
    }
}
