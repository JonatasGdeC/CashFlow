using System.Globalization;
using CashFlow.Application.Utils.GenerateReportPdf;
using CashFlow.Application.Utils.GenerateReportPdf.Fonts;
using CashFlow.Communication.Requests;
using CashFlow.Domain.Reports;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Services.LoggedUser;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Fonts;

namespace CashFlow.Application.UsesCases.Expense.Reports.Pdf;

public class GenerateExpensesReportPdfUseCase : IGenerateExpensesReportPdfUseCase
{
    private readonly IExpensesReadRepository _readRepository;
    private readonly ILoggedUser _loggedUser;

    public GenerateExpensesReportPdfUseCase(IExpensesReadRepository readRepository, ILoggedUser loggedUser)
    {
        _readRepository = readRepository;
        _loggedUser = loggedUser;
        GlobalFontSettings.FontResolver = new ExpenseReportFontResolver();
    }

    public async Task<byte[]> Execute(RequestFilterJson request)
    {
        Domain.Enitites.User currentUser = await _loggedUser.Get();
        List<Domain.Enitites.Expense>? response = await _readRepository.GetFilter(request: request, userId: currentUser.Id);
        
        if (response is null || response.Count == 0)
        {
            return [];
        }

        ExpensesReportPdfGenerator pdfGenerator = new(request: request, expenses: response);
        return pdfGenerator.Execute();
    }
}