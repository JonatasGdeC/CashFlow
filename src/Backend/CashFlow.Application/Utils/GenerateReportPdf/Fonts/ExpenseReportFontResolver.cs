using System.Reflection;
using PdfSharp.Fonts;

namespace CashFlow.Application.Utils.GenerateReportPdf.Fonts;

public class ExpenseReportFontResolver : IFontResolver
{
    public FontResolverInfo? ResolveTypeface(string familyName, bool bold, bool italic)
    {
        return new FontResolverInfo(faceName: familyName);
    }

    public byte[]? GetFont(string faceName)
    {
        Stream? stream = ReadFontFile(faceName: faceName);

        stream ??= ReadFontFile(faceName: FontHelper.DEFAULT);
        
        int length = (int)stream!.Length;
        byte[] data = new byte[length];
        
        stream.Read(buffer: data, offset: 0, count: length);
        
        return data;

    }

    private Stream? ReadFontFile(string faceName)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();

        return assembly.GetManifestResourceStream(name: $"CashFlow.Application.Utils.GenerateReportPdf.Fonts.{faceName}.ttf");
    }
}