using System.Globalization;
using CashFlow.Application.UsesCases.Expense.Reports.Pdf.Fonts;
using CashFlow.Communication.Requests;
using CashFlow.Domain.Reports;
using CashFlow.Domain.Repositories.Expenses;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Fonts;

namespace CashFlow.Application.UsesCases.Expense.Reports.Pdf;

public class GenerateExpensesReportPdfUseCase : IGenerateExpensesReportPdfUseCase
{
    private readonly IExpensesReadRepository _readRepository;

    public GenerateExpensesReportPdfUseCase(IExpensesReadRepository readRepository)
    {
        _readRepository = readRepository;
        GlobalFontSettings.FontResolver = new ExpenseReportFontResolver();
    }

    public async Task<byte[]> Execute(RequestInformationReportJson request)
    {
        List<Domain.Enitites.Expense>? response = await _readRepository.GetFilter(request: request);
        if (response is null || response.Count == 0)
        {
            return [];
        }

        Document document = CreateDocument(request: request);
        Section section = CreatePage(document: document);

        TotalSpendSection(section: section, request: request, response: response);

        foreach (Domain.Enitites.Expense expense in response)
        {
            Table table = CreateTable(section: section);
            table.AddRow().Height = 30;
            
            AddTableHeader(table: table, title: expense.Title);
            AddTableBody(table: table, expense: expense);
            AddTableFooter(table: table, expense: expense);
        }

        return RenderDocument(document: document);
    }

    private Document CreateDocument(RequestInformationReportJson request)
    {
        Document document = new();

        document.Info.Title = $"{ResourceReportGenerationMessage.EXPENSES_FOR} {request.Date.ToString(format: "Y")}";
        document.Info.Author = "Jônatas Carvalho";

        Style? style = document.Styles[styleName: "Normal"];
        style!.Font.Name = FontHelper.RALEWAY_REGULAR;


        return document;
    }

    private Section CreatePage(Document document)
    {
        Section section = document.AddSection();

        section.PageSetup = document.DefaultPageSetup.Clone();
        section.PageSetup.PageFormat = PageFormat.A4;
        section.PageSetup.LeftMargin = 40;
        section.PageSetup.RightMargin = 40;
        section.PageSetup.TopMargin = 60;
        section.PageSetup.BottomMargin = 60;

        return section;
    }

    private void TotalSpendSection(Section section, RequestInformationReportJson request, List<Domain.Enitites.Expense> response)
    {
        Paragraph paragraph = section.AddParagraph();
        string text = $"{ResourceReportGenerationMessage.EXPENSES_FOR} {request.Date.ToString(format: "Y")}";
        paragraph.AddFormattedText(text: text, font: new Font { Name = FontHelper.RALEWAY_REGULAR, Size = 15 });
        paragraph.AddLineBreak();

        decimal totalExpense = response.Sum(selector: expense => expense.Amount);
        paragraph.AddFormattedText(text: totalExpense.ToString(format: "C", provider: new CultureInfo(name: "pt-BR")), font: new Font { Name = FontHelper.WORKSANS_BLACK, Size = 50 });
    }

    private Table CreateTable(Section section)
    {
        Table table = section.AddTable();
        table.AddColumn(width: "195").Format.Alignment = ParagraphAlignment.Left;
        table.AddColumn(width: "80").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn(width: "120").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn(width: "120").Format.Alignment = ParagraphAlignment.Right;

        return table;
    }

    private void AddTableHeader(Table table, string title)
    {
        Row row = table.AddRow();
        row.Height = 25;
            
        row.Cells[index: 0].AddParagraph(paragraphText: title);
        row.Cells[index: 0].Format.Font = new Font{ Name = FontHelper.RALEWAY_BLACK, Size = 15, Color = ColorsHelper.BLACK };
        row.Cells[index: 0].Shading.Color = ColorsHelper.RED_LIGHT;
        row.Cells[index: 0].VerticalAlignment = VerticalAlignment.Center;
        row.Cells[index: 0].MergeRight = 2;
        row.Cells[index: 0].Format.LeftIndent = 20;
            
        row.Cells[index: 3].AddParagraph(paragraphText: ResourceReportGenerationMessage.AMOUNT);
        row.Cells[index: 3].Format.Font = new Font{ Name = FontHelper.RALEWAY_BLACK, Size = 15, Color = ColorsHelper.WHITE };
        row.Cells[index: 3].Shading.Color = ColorsHelper.RED_DARK;
        row.Cells[index: 3].VerticalAlignment = VerticalAlignment.Center;
        row.Cells[index: 3].Format.RightIndent = 20;
    }

    private void AddTableBody(Table table, Domain.Enitites.Expense expense)
    {
        Row row = table.AddRow();
        row.Height = 25;
        
        row.Cells[index: 0].AddParagraph(paragraphText: expense.Date.ToString(format: "D"));
        row.Cells[index: 0].Format.LeftIndent = 20;
        
        row.Cells[index: 1].AddParagraph(paragraphText: expense.Date.ToString(format: "T"));
        
        row.Cells[index: 2].AddParagraph(paragraphText: TreatPaymentType.Execute(paymentType: expense.PaymentType));
        
        AddTableBodyDefaultStyle(row: row);
        
        row.Cells[index: 3].AddParagraph(paragraphText: $"- {expense.Amount.ToString(format: "C", provider: new CultureInfo(name: "pt-BR"))}");
        row.Cells[index: 3].Format.Font = new Font{ Name = FontHelper.WORKSANS_REGULAR, Size = 14, Color = ColorsHelper.BLACK };
        row.Cells[index: 3].Shading.Color = ColorsHelper.WHITE;
        row.Cells[index: 3].VerticalAlignment = VerticalAlignment.Center;
    }
    private void AddTableBodyDefaultStyle(Row row)
    {
        for (int i = 0; i <= 2; i++)
        {
            row.Cells[index: i].Format.Font = new Font{ Name = FontHelper.WORKSANS_REGULAR, Size = 12, Color = ColorsHelper.BLACK };
            row.Cells[index: i].Shading.Color = ColorsHelper.GREEN_DARK;
            row.Cells[index: i].VerticalAlignment = VerticalAlignment.Center;
        }
    }

    private void AddTableFooter(Table table, Domain.Enitites.Expense expense)
    {
        if (!string.IsNullOrEmpty(value: expense.Description))
        {
            Row descriptionRow = table.AddRow();
            descriptionRow.Height = 25;
            descriptionRow.Cells[index: 0].AddParagraph(paragraphText: expense.Description);
            descriptionRow.Cells[index: 0].Format.Font = new Font{ Name = FontHelper.WORKSANS_REGULAR, Size = 12, Color = ColorsHelper.BLACK };
            descriptionRow.Cells[index: 0].Shading.Color = ColorsHelper.GREEN_LIGHT;
            descriptionRow.Cells[index: 0].VerticalAlignment = VerticalAlignment.Center;
            descriptionRow.Cells[index: 0].MergeRight = 3;
            descriptionRow.Cells[index: 0].Format.LeftIndent = 20;
        }
    }
    
    private byte[] RenderDocument(Document document)
    {
        PdfDocumentRenderer render = new()
        {
            Document = document,
        };

        render.RenderDocument();

        using MemoryStream file = new();
        render.PdfDocument.Save(stream: file);

        return file.ToArray();
    }
}