using System.Globalization;
using CashFlow.Application.Utils.GenerateReportPdf;
using CashFlow.Application.Utils.GenerateReportPdf.Fonts;
using CashFlow.Communication.Requests;
using CashFlow.Domain.Reports;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;

namespace CashFlow.Application.UsesCases.Expense.Reports.Pdf;

public class ExpensesReportPdfGenerator(RequestFilterJson request, List<Domain.Enitites.Expense> expenses) : GenerateReportPdf<Domain.Enitites.Expense>
{
    protected override string Title()
    {
        return $"{ResourceReportGenerationMessage.EXPENSES_FOR} {request.Date.ToString(format: "Y")}";
    }

    protected override void TotalSpendSection(Section section)
    {
        Paragraph paragraph = section.AddParagraph();
        string text = $"{ResourceReportGenerationMessage.EXPENSES_FOR} {request.Date.ToString(format: "Y")}";
        paragraph.AddFormattedText(text: text, font: new Font { Name = FontHelper.RALEWAY_REGULAR, Size = 15 });
        paragraph.AddLineBreak();
        decimal totalExpense = expenses?.Sum(selector: expense => expense.Amount) ?? 0;
        paragraph.AddFormattedText(text: totalExpense.ToString(format: "C", provider: new CultureInfo(name: "pt-BR")), font: new Font { Name = FontHelper.WORKSANS_BLACK, Size = 50 });
    }

    protected override Table CreateTable(Section section)
    {
        Table table = section.AddTable();
        table.AddColumn(width: "195").Format.Alignment = ParagraphAlignment.Left;
        table.AddColumn(width: "80").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn(width: "120").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn(width: "120").Format.Alignment = ParagraphAlignment.Right;

        return table;
    }

    protected override void AddTableHeader(Table table, Domain.Enitites.Expense item)
    {
        Row row = table.AddRow();
        row.Height = 25;
            
        row.Cells[index: 0].AddParagraph(paragraphText: item.Title);
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

    protected override void AddTableBody(Table table, Domain.Enitites.Expense item)
    {
        Row row = table.AddRow();
        row.Height = 25;
        
        row.Cells[index: 0].AddParagraph(paragraphText: item.Date.ToString(format: "D"));
        row.Cells[index: 0].Format.LeftIndent = 20;
        
        row.Cells[index: 1].AddParagraph(paragraphText: item.Date.ToString(format: "T"));
        
        row.Cells[index: 2].AddParagraph(paragraphText: TreatPaymentType.Execute(paymentType: item.PaymentType));
        
        AddTableBodyDefaultStyle(row: row);
        
        row.Cells[index: 3].AddParagraph(paragraphText: $"- {item.Amount.ToString(format: "C", provider: new CultureInfo(name: "pt-BR"))}");
        row.Cells[index: 3].Format.Font = new Font{ Name = FontHelper.WORKSANS_REGULAR, Size = 14, Color = ColorsHelper.BLACK };
        row.Cells[index: 3].Shading.Color = ColorsHelper.WHITE;
        row.Cells[index: 3].VerticalAlignment = VerticalAlignment.Center;
    }

    protected override void AddTableBodyDefaultStyle(Row row)
    {
        for (int i = 0; i <= 2; i++)
        {
            row.Cells[index: i].Format.Font = new Font{ Name = FontHelper.WORKSANS_REGULAR, Size = 12, Color = ColorsHelper.BLACK };
            row.Cells[index: i].Shading.Color = ColorsHelper.GREEN_DARK;
            row.Cells[index: i].VerticalAlignment = VerticalAlignment.Center;
        }
    }

    protected override void AddTableFooter(Table table, Domain.Enitites.Expense item)
    {
        if (!string.IsNullOrEmpty(value: item.Description))
        {
            Row descriptionRow = table.AddRow();
            descriptionRow.Height = 25;
            descriptionRow.Cells[index: 0].AddParagraph(paragraphText: item.Description);
            descriptionRow.Cells[index: 0].Format.Font = new Font{ Name = FontHelper.WORKSANS_REGULAR, Size = 12, Color = ColorsHelper.BLACK };
            descriptionRow.Cells[index: 0].Shading.Color = ColorsHelper.GREEN_LIGHT;
            descriptionRow.Cells[index: 0].VerticalAlignment = VerticalAlignment.Center;
            descriptionRow.Cells[index: 0].MergeRight = 3;
            descriptionRow.Cells[index: 0].Format.LeftIndent = 20;
        }
    }


    public byte[] Execute() => Build(request: request, items: expenses);
}