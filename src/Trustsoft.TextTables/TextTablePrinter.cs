// -------------------------Copyright © 2025 M.Sukhanov. All rights reserved.-------------------------
//  <copyright file="TextTablePrinter.cs" author="M.Sukhanov">
//      Copyright © 2025 M.Sukhanov. All rights reserved.
//  </copyright>
//  <date>18.01.2025</date>
// -------------------------Copyright © 2025 M.Sukhanov. All rights reserved.-------------------------

namespace Trustsoft.TextTables;

using Trustsoft.TextTables.Contracts;

internal static class TextTablePrinter
{
    /// <summary>
    ///   Returns a new string that center aligns the characters in a
    ///   string by padding them on the left and right with a specified
    ///   character, of a specified total length.
    /// </summary>
    /// <param name="source"> The source string. </param>
    /// <param name="desiredWidth"> The number of characters to pad the source string. </param>
    /// <param name="paddingChar"> The padding character. </param>
    /// <returns>
    ///   The modified source string padded with as many paddingChar
    ///   characters needed to create a length of totalWidth.
    /// </returns>
    private static string PadCenter(string source, int desiredWidth, char paddingChar = ' ')
    {
        var spaces = desiredWidth - source.Length;
        var padLeft = spaces / 2 + source.Length;
        return source.PadLeft(padLeft, paddingChar).PadRight(desiredWidth, paddingChar);
    }

    private static string CreateDivider(PrintContext ctx, int width)
    {
        return $"{ctx.Indent}+{new string('-', width)}+";
    }

    private static string BuildLineFormat(PrintContext ctx, char left, char right,
                                          char columnDivider, char indentChar)
    {
        string Align(TableColumn column, int i, int wide)
        {
            return $"{{{i},{(column.Alignment == Alignment.Left ? "-" : "")}{wide}}}";
        }

        var indent = ctx.Indent;
        var contentIndent = new string(indentChar, ctx.ContentIndent.Length);
        var columnLengths = ctx.ColumnLengths;
        var formats = ctx.Table.Columns.Select((column, i) => Align(column, i, columnLengths[i]));
        var lineFormat = formats.Aggregate((l, r) => $"{l}{contentIndent}{columnDivider}{contentIndent}{r}");
        return $"{indent}{left}{contentIndent}{lineFormat}{contentIndent}{right}";
    }

    private static int GetTableWidth(ITextTable table)
    {
        // add borders
        int width = 2;
        
        // add column lengths
        width += GetColumnWidths(table).Sum();
        
        // add columns content indent left + right
        width += table.Columns.Count * table.Options.ContentIndent * 2;
        
        // add column separators
        width += table.Columns.Count - 1;
        
        return width;
    }

    private static IEnumerable<int> GetColumnWidths(ITextTable table)
    {
        for (var index = 0; index < table.Columns.Count; index++)
        {
            var result = table.Rows
                              .Select(row => row[index])
                              .Select(rowValue => rowValue?.ToString() ?? string.Empty)
                              .Union([table.Columns[index].Name])
                              .Select(s => s.Length).Max();

            yield return result;
        }
    }

    private static IEnumerable<int> GetFooterPartsWidths(ITextTable table)
    {
        return table.Footer.Select(part => part?.ToString())
                    .Select(s => s?.Length ?? 0)
                    .ToList();
    }

    private static int GetFooterWidth(ITextTable table)
    {
        // left and right borders 
        int width = 2;

        // add footer parts lengths
        width += GetFooterPartsWidths(table).Sum();

        // add parts content indent left + right
        width += table.Footer.Count * (table.Options.ContentIndent * 2);

        // add footer parts separators
        width += table.Footer.Count - 1;

        return width;
    }

    private static void PrintRuler(PrintContext ctx)
    {
        var s1 = ctx.Indent;
        var s2 = ctx.Indent;

        for (var i = 0; i < ctx.TableWidth; i++)
        {
            var x = i + 1;
            var y = x % 10;

            s1 += y == 0 ? $"{x / 10}" : " ";
            s2 += $"{y}";
        }

        ctx.OutputTo.WriteLine(s1);
        ctx.OutputTo.WriteLine(s2);
    }

    private static void PrintTitle(PrintContext ctx)
    {
        var titleArea = ctx.TableWidth - 2;

        if (ctx.ShouldShowTopBorder)
        {
            var divider = CreateDivider(ctx, titleArea);
            ctx.OutputTo.WriteLine(divider);
        }

        var lineFormat = $"{ctx.Indent}|{PadCenter(ctx.Table.Title, titleArea)}|";
        ctx.OutputTo.WriteLine(lineFormat);
    }

    private static void PrintHeader(PrintContext ctx)
    {
        if (ctx.ShouldShowTopBorder || ctx.ShouldShowTitle)
        {
            var div = BuildLineFormat(ctx, '+', '+', '+', '-');
            var objects = ctx.ColumnLengths.Select(object (number) => new string('-', number)).ToArray();
            ctx.OutputTo.WriteLine(div, objects);
        }

        var fmt = BuildLineFormat(ctx, '|', '|', '|', ' ');
        var columnNames = ctx.Table.Columns.Select(object (column) => column.Name);
        ctx.OutputTo.WriteLine(fmt, columnNames.ToArray());
    }

    private static void PrintRows(PrintContext ctx)
    {
        var div = BuildLineFormat(ctx, '+', '+', '+', '-');
        var objects = ctx.ColumnLengths.Select(object (number) => new string('-', number)).ToArray();
        var divider = string.Format(div, objects);

        var fmt = BuildLineFormat(ctx, '|', '|', '|', ' ');
        if (ctx.ShouldShowTopBorder || ctx.ShouldShowTitle || ctx.ShouldShowHeader)
        {
            ctx.OutputTo.WriteLine(divider);
        }

        foreach (var row in ctx.Table.Rows.SkipLast(1))
        {
            ctx.OutputTo.WriteLine(fmt, row);

            if (ctx.ShouldShowRowSeparator)
            {
                ctx.OutputTo.WriteLine(divider);
            }
        }

        ctx.OutputTo.WriteLine(fmt, ctx.Table.Rows.Last());

        if (ctx.ShouldShowFooter)
        {
            ctx.OutputTo.WriteLine(divider);
        }
        else
        {
            if (ctx.ShouldShowBottomBorder)
            {
                ctx.OutputTo.WriteLine(divider);
            }
        }
    }

    private static void PrintFooter(PrintContext ctx)
    {
        var footerParts = ctx.Table.Footer.Select(part => part?.ToString()).ToList();
        var footerWidths = footerParts.Select(s => s?.Length ?? 0)
                                      .ToList();

        var footerDesiredWidth = GetFooterWidth(ctx.Table) - footerParts.First()!.Length;
        var footerFirstPartDesiredWidth = ctx.TableWidth - footerDesiredWidth;
        footerWidths[0] = footerFirstPartDesiredWidth;
        var lineFormat = ctx.Indent;
        var indices = Enumerable.Range(0, footerParts.Count);
        lineFormat += indices
                      .Select(i => "|" + ctx.ContentIndent + "{" + i + "," + (i == 0 ? "-" : "") + footerWidths[i] + "}")
                      .Aggregate((a, b) => $"{a}{ctx.ContentIndent}{b}") + ctx.ContentIndent + "|";

        var footerArea = ctx.TableWidth - 2;
        ctx.OutputTo.WriteLine(lineFormat, ctx.Table.Footer.ToArray());

        if (ctx.ShouldShowBottomBorder)
        {
            var divider = CreateDivider(ctx, footerArea);
            ctx.OutputTo.WriteLine(divider);
        }
    }

    private static PrintContext CreateContext(ITextTable table, TableLayout layout, TextWriter output)
    {
        var tableWidth = GetTableWidth(table);
        var columnLengths = GetColumnWidths(table).ToList();
        return PrintContext.Create(table, layout, output, columnLengths, tableWidth);
    }

    public static void PrintTo(TextWriter output, ITextTable table, TableLayout layout)
    {
        var ctx = CreateContext(table, layout, output);

        // print ruler
        if (table.Options.ShowRuler)
        {
            PrintRuler(ctx);
        }
        
        // print title
        if (ctx.ShouldShowTitle)
        {
            PrintTitle(ctx);
        }
        
        // print header
        if (ctx.ShouldShowHeader)
        {
            PrintHeader(ctx);
        }

        // print rows
        PrintRows(ctx);

        // print footer
        if (ctx.ShouldShowFooter)
        {
            PrintFooter(ctx);
        }
    }
}