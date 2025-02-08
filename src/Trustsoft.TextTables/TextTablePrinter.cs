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

        var contentIndent = new string(indentChar, ctx.ContentIndentSize);
        var formats = ctx.Table.Columns.Select((column, i) => Align(column, i, ctx.ColumnWidths[i]));
        var lineFormat = formats.Aggregate((l, r) => $"{l}{contentIndent}{columnDivider}{contentIndent}{r}");
        return $"{ctx.Indent}{left}{contentIndent}{lineFormat}{contentIndent}{right}";
    }

    private static string BuildRowLineFormat(PrintContext ctx, object?[] row,
                                             char left, char right, char columnDivider, char indentChar)
    {
        string Align(TableColumn column, int i, int wide, object? cell)
        {
            switch (column.Alignment)
            {
                case Alignment.Left:
                {
                    return $"{{{i},-{wide}}}";
                }
                case Alignment.Right:
                {
                    return $"{{{i},{wide}}}";
                }

                case Alignment.Center:
                {
                    var value = cell?.ToString() ?? string.Empty;
                    var spaces = wide - value.Length;

                    var align = $"{{{i}}}";

                    if (spaces == 0)
                    {
                        return align;
                    }
                    
                    var padLeft = spaces / 2;
                    var leftPart = new string(' ', padLeft);
                    var rightPart = new string(' ', spaces - padLeft);
                    return $"{leftPart}{align}{rightPart}";
                }

                default:
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        var contentIndent = new string(indentChar, ctx.ContentIndentSize);
        var formats = ctx.Table.Columns.Select((column, i) => Align(column, i, ctx.ColumnWidths[i], row[i]));
        var lineFormat = formats.Aggregate((l, r) => $"{l}{contentIndent}{columnDivider}{contentIndent}{r}");
        return $"{ctx.Indent}{left}{contentIndent}{lineFormat}{contentIndent}{right}";
    }

    private static int GetTableWidth(ITextTable table)
    {
        // add borders
        int width = 2;

        var columnLengths = GetColumnWidths(table);

        // add column lengths
        width += columnLengths.Sum();
        
        // add columns content indent left + right
        width += table.Columns.Count * table.Options.ContentIndent * 2;
        
        // add column separators
        width += table.Columns.Count - 1;
        
        return width;
    }

    private static IEnumerable<int> GetColumnWidths(ITextTable table)
    {
        var columnLengths = GetContentWidths(table);

        if (table.Options.ShowHeader)
        {
            return columnLengths.Zip(GetHeaderWidths(table), Math.Max);
        }

        return columnLengths;
    }

    private static IEnumerable<int> GetContentWidths(ITextTable table)
    {
        for (var index = 0; index < table.Columns.Count; index++)
        {
            yield return table.Rows
                              .Select(row => row[index])
                              .Select(rowValue => rowValue?.ToString() ?? string.Empty)
                              .Select(s => s.Length).Max();
        }
    }

    private static IEnumerable<int> GetHeaderWidths(ITextTable table)
    {
        return table.Columns
                    .Select(column => column.Name)
                    .Select(name => name.Length);
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

        var lineFormat = $"{ctx.Indent}|{ctx.Table.Title.PadCenter(titleArea)}|";
        ctx.OutputTo.Write(lineFormat);
    }

    private static void PrintHeader(PrintContext ctx)
    {
        if (ctx.ShouldShowTopBorder || ctx.ShouldShowTitle)
        {
            var div = BuildLineFormat(ctx, '+', '+', '+', '-');
            var objects = ctx.ColumnWidths.Select(number => new string('-', number)).Cast<object>().ToArray();
            if (ctx.ShouldShowTitle)
            {
                ctx.OutputTo.WriteLine();
            }

            ctx.OutputTo.WriteLine(div, objects);
        }

        var columnNames = ctx.Table.Columns.Select(column => column.Name).Cast<object>().ToArray();
        var fmt = BuildRowLineFormat(ctx, columnNames, '|', '|', '|', ' ');
        ctx.OutputTo.Write(fmt, columnNames);
    }

    private static void PrintRows(PrintContext ctx)
    {
        var div = BuildLineFormat(ctx, '+', '+', '+', '-');
        var objects = ctx.ColumnWidths.Select(object (number) => new string('-', number)).ToArray();
        var divider = string.Format(div, objects);

        if (ctx.ShouldShowTopBorder || ctx.ShouldShowTitle || ctx.ShouldShowHeader)
        {
            ctx.OutputTo.WriteLine();
            ctx.OutputTo.Write(divider);
        }

        string fmt;
        foreach (var row in ctx.Table.Rows.SkipLast(1))
        {
            fmt = BuildRowLineFormat(ctx, row, '|', '|', '|', ' ');
            ctx.OutputTo.WriteLine();
            ctx.OutputTo.Write(fmt, row);

            if (ctx.ShouldShowRowSeparator)
            {
                ctx.OutputTo.WriteLine();
                ctx.OutputTo.Write(divider);
            }
        }

        var r = ctx.Table.Rows.Last();
        fmt = BuildRowLineFormat(ctx, r, '|', '|', '|', ' ');
        ctx.OutputTo.WriteLine();
        ctx.OutputTo.Write(fmt, r);

        if (ctx.ShouldShowFooter || ctx.ShouldShowBottomBorder)
        {
            ctx.OutputTo.WriteLine();
            ctx.OutputTo.Write(divider);
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
        var indices = Enumerable.Range(0, footerParts.Count).ToList();
        var aligns = indices.Select(i => i == 0 ? "-" : "").ToArray();
        lineFormat += indices.Select(i => $"|{ctx.ContentIndent}{{{i},{aligns[i]}{footerWidths[i]}}}")
                             .Aggregate((a, b) => $"{a}{ctx.ContentIndent}{b}") + ctx.ContentIndent + "|";

        var footerArea = ctx.TableWidth - 2;
        ctx.OutputTo.WriteLine();
        ctx.OutputTo.Write(lineFormat, ctx.Table.Footer.ToArray());

        if (ctx.ShouldShowBottomBorder)
        {
            var divider = CreateDivider(ctx, footerArea);
            ctx.OutputTo.WriteLine();
            ctx.OutputTo.Write(divider);
        }
    }

    private static PrintContext CreateContext(ITextTable table, TableLayout layout, TextWriter output)
    {
        var tableWidth = GetTableWidth(table);
        var columnWidths = GetColumnWidths(table);

        return new PrintContext(table: table,
                                outputTo: output,
                                layout: layout,
                                columnWidths: columnWidths.ToList(),
                                tableWidth: tableWidth);
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