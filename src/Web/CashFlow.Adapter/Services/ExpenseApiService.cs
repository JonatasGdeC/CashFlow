using CashFlow.Adapter.Utils;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Requests.Expense;
using CashFlow.Communication.Response;
using CashFlow.Communication.Response.Expense;

namespace CashFlow.Adapter.Services;

public class ExpenseApiService(HttpClient httpClient) : ApiServiceBase(httpClient: httpClient)
{
    private const string Method = "api/Expenses";

    public async Task<ResponseRegisterExpenseJson> Register(RequestRegisterExpenseJson request)
    {
        return await PostAsync<RequestRegisterExpenseJson, ResponseRegisterExpenseJson>(uri: Method, request: request);
    }

    public async Task<ResponseGetAllExpensesJson> GetAll(RequestFilterJson request)
    {
        return await GetNullableAsync<ResponseGetAllExpensesJson>(uri: $"{Method}?{RequestFilterJsonBuildQueryString.Execute(request: request)}") ?? new ResponseGetAllExpensesJson();
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

    public async Task<ResponseExpenseJson> GetById(Guid id)
    {
        return await GetAsync<ResponseExpenseJson>(uri: $"{Method}/{id}");
    }

    public async Task Update(Guid id, RequestRegisterExpenseJson request)
    {
        await PutAsync(uri: $"{Method}/{id}", request: request);
    }

    public async Task Delete(Guid id)
    {
        await DeleteAsync(uri: $"{Method}/{id}");
    }
}
