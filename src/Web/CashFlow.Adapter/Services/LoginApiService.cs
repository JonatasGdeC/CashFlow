using CashFlow.Communication.Requests.User;
using CashFlow.Communication.Response.User;

namespace CashFlow.Adapter.Services;

public class LoginApiService(HttpClient httpClient) : ApiServiceBase(httpClient: httpClient)
{
    private const string Method = "api/Login";

    public async Task<ResponseRegisterUserJson> Login(RequestLoginJson request)
    {
        return await PostAsync<RequestLoginJson, ResponseRegisterUserJson>(uri: Method, request: request);
    }
}