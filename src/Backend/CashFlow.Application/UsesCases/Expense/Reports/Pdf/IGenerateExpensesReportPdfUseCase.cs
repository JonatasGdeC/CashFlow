using CashFlow.Communication.Requests;

namespace CashFlow.Application.UsesCases.Expense.Reports.Pdf;

public interface IGenerateExpensesReportPdfUseCase
{
    Task<byte[]> Execute(RequestFilterJson request);
}