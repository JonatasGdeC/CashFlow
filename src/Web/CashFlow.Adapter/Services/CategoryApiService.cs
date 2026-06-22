using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests.Category;
using CashFlow.Communication.Response.Category;

namespace CashFlow.Adapter.Services;

public class CategoryApiService(HttpClient httpClient) : ApiServiceBase(httpClient: httpClient)
{
    private const string BaseUri = "api/categories";

    public async Task<ResponseRegisterCategoryJson> Register(RequestRegisterCategoryJson request)
    {
        return await PostAsync<RequestRegisterCategoryJson, ResponseRegisterCategoryJson>(uri: BaseUri, request: request);
    }

    public async Task<ResponseGetAllCategoriesJson?> GetAll(CategoryType categoryType)
    {
        return await GetNullableAsync<ResponseGetAllCategoriesJson>(uri: $"{BaseUri}?categoryType={categoryType}");
    }

    public async Task<ResponseCategoryJson> GetById(Guid id)
    {
        return await GetAsync<ResponseCategoryJson>(uri: $"{BaseUri}/{id}");
    }

    public async Task Update(Guid id, RequestRegisterCategoryJson request)
    {
        await PutAsync(uri: $"{BaseUri}/{id}", request: request);
    }

    public async Task Delete(Guid id)
    {
        await DeleteAsync(uri: $"{BaseUri}/{id}");
    }
}