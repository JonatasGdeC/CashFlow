using CashFlow.Communication.Requests;

namespace CashFlow.Application.UsesCases.Income.Reports.Excel;

public interface IGenerateIncomesReportExcelUseCase
{
    Task<byte[]> Execute(RequestFilterJson request);
}