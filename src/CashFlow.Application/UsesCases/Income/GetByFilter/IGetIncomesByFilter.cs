using CashFlow.Communication.Requests;
using CashFlow.Communication.Response.Income;

namespace CashFlow.Application.UsesCases.Income.GetByFilter;

public interface IGetIncomesByFilter
{
    Task<ResponseGetAllIncomesJson> Execute(RequestFilterJson request);
}