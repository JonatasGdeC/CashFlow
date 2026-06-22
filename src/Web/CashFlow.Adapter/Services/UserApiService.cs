using CashFlow.Communication.Requests.User;
using CashFlow.Communication.Response.User;

namespace CashFlow.Adapter.Services;

public class UserApiService(HttpClient httpClient) : ApiServiceBase(httpClient: httpClient)
{
    private const string Method = "api/User";
    
    public async Task<ResponseRegisterUserJson> Register(RequestRegisterUserJson request)
    {
        return await PostAsync<RequestRegisterUserJson, ResponseRegisterUserJson>(uri: Method, request: request);
    }

    public async Task<ResponseUserProfileJson> GetProfile()
    {
        return await GetAsync<ResponseUserProfileJson>(uri: Method);
    }

    public async Task UpdateProfile(RequestUpdateUserJson request)
    {
        await PutAsync(uri: Method, request: request);
    }

    public async Task ChangePassword(RequestChangePasswordJson request)
    {
        await PutAsync(uri: $"{Method}/change-password", request: request);
    }

    public async Task Delete()
    {
        await DeleteAsync(uri: Method);
    }
}