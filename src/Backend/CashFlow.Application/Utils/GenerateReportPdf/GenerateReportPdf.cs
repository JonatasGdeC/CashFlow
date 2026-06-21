using CashFlow.Application.Utils.GenerateReportPdf.Fonts;
using CashFlow.Communication.Requests;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;

namespace CashFlow.Application.Utils.GenerateReportPdf;

public abstract class GenerateReportPdf<T>
{
    protected abstract string Title();
    protected abstract void TotalSpendSection(Section section);
    protected abstract Table CreateTable(Section section);
    protected abstract void AddTableHeader(Table table, T item);
    protected abstract void AddTableBody(Table table, T item);
    protected abstract void AddTableBodyDefaultStyle(Row row);
    protected abstract void AddTableFooter(Table table, T item);

    private void BuildInfos(List<T> items, Section section)
    {
        foreach (T item in items)
        {
            Table table = CreateTable(section: section);
            table.AddRow().Height = 30;

            AddTableHeader(table: table, item: item);
            AddTableBody(table: table, item: item);
            AddTableFooter(table: table, item: item);
        }
    }

    protected byte[] Build(RequestFilterJson request, List<T> items)
    {
        Document document = CreateDocument();
        Section section = CreatePage(document: document);

        TotalSpendSection(section: section);

        BuildInfos(items: items, section: section);

        return RenderDocument(document: document);
    }


    private Document CreateDocument()
    {
        Document document = new();

        document.Info.Title = Title();
        document.Info.Author = "Jônatas Carvalho";
        Style? style = document.Styles[styleName: "Normal"];
        style!.Font.Name = FontHelper.RALEWAY_REGULAR;

        return document;
    }

    private static Section CreatePage(Document document)
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

    private static byte[] RenderDocument(Document document)
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