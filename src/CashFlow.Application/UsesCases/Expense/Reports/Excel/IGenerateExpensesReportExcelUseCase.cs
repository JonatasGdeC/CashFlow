using CashFlow.Communication.Requests;

namespace CashFlow.Application.UsesCases.Expense.Reports.Excel;

public interface IGenerateExpensesReportExcelUseCase
{
    Task<byte[]> Execute(RequestFilterJson request);
}