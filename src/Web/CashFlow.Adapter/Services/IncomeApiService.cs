using CashFlow.Adapter.Utils;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Requests.Income;
using CashFlow.Communication.Response;
using CashFlow.Communication.Response.Income;

namespace CashFlow.Adapter.Services;

public class IncomeApiService(HttpClient httpClient) : ApiServiceBase(httpClient: httpClient)
{
    private const string Method = "api/incomes";

    public async Task<ResponseRegisterIncomeJson> Register(RequestRegisterIncomeJson request)
    {
        return await PostAsync<RequestRegisterIncomeJson, ResponseRegisterIncomeJson>(uri: Method, request: request);
    }

    public async Task<ResponseGetAllIncomesJson> GetAll(RequestFilterJson request)
    {
        return await GetNullableAsync<ResponseGetAllIncomesJson>(uri: $"{Method}?{RequestFilterJsonBuildQueryString.Execute(request: request)}") ?? new ResponseGetAllIncomesJson();
    }
    
    public async Task<ResponseDashboardJson> GetDashboard()
    {
        return await GetAsync<ResponseDashboardJson>(uri: $"{Method}/dashboard");
    }
    
    public async Task<byte[]?> GetExcel(RequestFilterJson request)
    {
        return await GetFileAsync(uri: $"{Method}/report/excel?{RequestFilterJsonBuildQueryString.Execute(request: request)}");
    }

    public async Task<byte[]?> GetPdf(RequestFilterJson request)
    {
        return await GetFileAsync(uri: $"{Method}/report/pdf?{RequestFilterJsonBuildQueryString.Execute(request: request)}");
    }

    public async Task<ResponseIncomeJson> GetById(Guid id)
    {
        return await GetAsync<ResponseIncomeJson>(uri: $"{Method}/{id}");
    }

    public async Task Update(Guid id, RequestRegisterIncomeJson request)
    {
        await PutAsync(uri: $"{Method}/{id}", request: request);
    }

    public async Task Delete(Guid id)
    {
        await DeleteAsync(uri: $"{Method}/{id}");
    }
}
