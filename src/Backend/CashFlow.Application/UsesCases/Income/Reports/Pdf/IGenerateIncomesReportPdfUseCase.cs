using CashFlow.Communication.Requests;

namespace CashFlow.Application.UsesCases.Income.Reports.Pdf;

public interface IGenerateIncomesReportPdfUseCase
{
    Task<byte[]> Execute(RequestFilterJson request);
}