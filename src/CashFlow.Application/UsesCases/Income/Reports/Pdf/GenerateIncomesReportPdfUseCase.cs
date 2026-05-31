using CashFlow.Application.Utils.GenerateReportPdf.Fonts;
using CashFlow.Communication.Requests;
using CashFlow.Domain.Repositories.Incomes;
using CashFlow.Domain.Services.LoggedUser;
using PdfSharp.Fonts;

namespace CashFlow.Application.UsesCases.Income.Reports.Pdf;

public class GenerateIncomesReportPdfUseCase : IGenerateIncomesReportPdfUseCase
{
    private readonly IIncomesReadRepository _readRepository;
    private readonly ILoggedUser _loggedUser;

    public GenerateIncomesReportPdfUseCase(IIncomesReadRepository readRepository, ILoggedUser loggedUser)
    {
        _readRepository = readRepository;
        _loggedUser = loggedUser;
        GlobalFontSettings.FontResolver = new ExpenseReportFontResolver();
    }

    public async Task<byte[]> Execute(RequestFilterJson request)
    {
        Domain.Enitites.User currentUser = await _loggedUser.Get();
        List<Domain.Enitites.Income>? response = await _readRepository.GetFilter(request: request, userId: currentUser.Id);
        
        if (response is null || response.Count == 0)
        {
            return [];
        }

        IncomesReportPdfGenerator pdfGenerator = new(request: request, incomes: response);
        return pdfGenerator.Execute();
    }
}