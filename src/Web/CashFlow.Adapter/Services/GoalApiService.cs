
using CashFlow.Communication.Requests.Goal;
using CashFlow.Communication.Response.Goal;

namespace CashFlow.Adapter.Services;

public class GoalApiService(HttpClient httpClient) : ApiServiceBase(httpClient: httpClient)
{
    private const string BaseUri = "api/goals";

    public async Task<ResponseRegisterGoalJson> Register(RequestRegisterGoalJson request)
    {
        return await PostAsync<RequestRegisterGoalJson, ResponseRegisterGoalJson>(uri: BaseUri, request: request);
    }

    public async Task<ResponseGoalJson> GetById(Guid id)
    {
        return await GetAsync<ResponseGoalJson>(uri: $"{BaseUri}/{id}");
    }

    public async Task<ResponseGoalJson> GetByCategoryId(Guid categoryId)
    {
        return await GetAsync<ResponseGoalJson>(uri: $"{BaseUri}/category/{categoryId}");
    }

    public async Task Update(Guid id, RequestRegisterGoalJson request)
    {
        await PutAsync(uri: $"{BaseUri}/{id}", request: request);
    }

    public async Task Delete(Guid id)
    {
        await DeleteAsync(uri: $"{BaseUri}/{id}");
    }
}