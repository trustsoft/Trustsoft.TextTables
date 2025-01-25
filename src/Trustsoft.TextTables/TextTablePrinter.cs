// -------------------------Copyright © 2025 M.Sukhanov. All rights reserved.-------------------------
//  <copyright file="TextTablePrinter.cs" author="M.Sukhanov">
//      Copyright © 2025 M.Sukhanov. All rights reserved.
//  </copyright>
//  <date>18.01.2025</date>
// -------------------------Copyright © 2025 M.Sukhanov. All rights reserved.-------------------------

namespace Trustsoft.TextTables;

using Trustsoft.TextTables.Contracts;

internal class TextTablePrinter
{
    private int GetTableWidth(ITextTable table)
    {
        // add borders
        int width = 2;
        
        // add column lengths
        width += this.GetColumnWidths(table).Sum();
        
        // add columns content indent left + right
        width += table.Columns.Count * table.Options.ContentIndent * 2;
        
        // add column separators
        width += table.Columns.Count - 1;
        
        return width;
    }
    
    private IEnumerable<int> GetColumnWidths(ITextTable table)
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

    private IEnumerable<int> GetFooterPartsWidths(ITextTable table)
    {
        return table.Footer.Select(part => part?.ToString())
                    .Select(s => s?.Length ?? 0)
                    .ToList();
    }

    private int GetFooterWidth(ITextTable table)
    {
        // left and right borders 
        int width = 2;

        // add footer parts lengths
        width += this.GetFooterPartsWidths(table).Sum();

        // add parts content indent left + right
        width += table.Footer.Count * (table.Options.ContentIndent * 2);

        // add footer parts separators
        width += table.Footer.Count - 1;

        return width;
    }

    private void PrintRuler(PrintContext ctx)
    {
        var s = ctx.Indent;

        for (var i = 0; i < ctx.TableWidth; i++)
        {
            var x = i + 1;
            var y = x % 10;

            s += y == 0 ? $"{x / 10}" : " ";
        }

        ctx.OutputTo.WriteLine(s);
        s = ctx.Indent;

        for (var i = 0; i < ctx.TableWidth; i++)
        {
            var x = i + 1;
            var y = x % 10;
            s += $"{y}";
        }

        ctx.OutputTo.WriteLine(s);
    }

    private void PrintTitle(PrintContext ctx)
    {
        var tableWidth = ctx.TableWidth;
        var titleArea = tableWidth - 2;

        if (ctx.ShouldShowTopBorder)
        {
            var divider = $"{ctx.Indent}+{new string('-', titleArea)}+";
            ctx.OutputTo.WriteLine("T" + divider[1..]);
        }

        var leftIndent = (titleArea - ctx.Table.Title.Length) / 2;
        var leftPart = new string(' ', leftIndent);
        var rightPart = new string(' ', (titleArea - ctx.Table.Title.Length) - leftIndent);
        var lineFormat = $"{ctx.Indent}|{leftPart}{ctx.Table.Title}{rightPart}|";
        ctx.OutputTo.WriteLine(lineFormat);
    }

    private void PrintHeader(PrintContext ctx)
    {
        string GetAlignmentSpecifier(TableColumn column)
        {
            return column.Alignment switch
            {
                Alignment.Left => "-",
                Alignment.Right => "",
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        var widths = this.GetColumnWidths(ctx.Table).ToList();

        var indentWide = 2 * ctx.Table.Options.ContentIndent;
        var contentAreas = widths.Select(w => w + indentWide).ToList();
        var div = ctx.Indent;
        div += Enumerable.Range(0, ctx.Table.Columns.Count)
                         .Select(i => "+{" + i + "," + "-" + contentAreas[i] + "}")
                         .Aggregate((a, b) => $"{a}{b}") + "+";

        var divider = string.Format(div, contentAreas.Select(object (length) => new string('-', length)).ToArray());

        if (ctx.ShouldShowTopBorder || ctx.ShouldShowTitle)
        {
            ctx.OutputTo.WriteLine("H" + divider[1..]);
        }

        var lineFormat = ctx.Indent;
        lineFormat += Enumerable.Range(0, ctx.Table.Columns.Count)
                                .Select(i => "|" + ctx.ContentIndent + "{" + i + "," +
                                             GetAlignmentSpecifier(ctx.Table.Columns[i]) + widths[i] + "}")
                                .Aggregate((a, b) => $"{a}{ctx.ContentIndent}{b}") + ctx.ContentIndent + "|";
        // print column names
        ctx.OutputTo.WriteLine(lineFormat, ctx.Table.Columns.Select(object (column) => column.Name).ToArray());
    }

    private void PrintRows(PrintContext ctx)
    {
        string GetAlignmentSpecifier(TableColumn column)
        {
            return column.Alignment switch
            {
                Alignment.Left => "-",
                Alignment.Right => "",
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        var columnWidths = this.GetColumnWidths(ctx.Table).ToList();
        var lineFormat = ctx.Indent;
        lineFormat += Enumerable.Range(0, ctx.Table.Columns.Count)
                                .Select(i => "|" + ctx.ContentIndent + "{" + i + "," +
                                             GetAlignmentSpecifier(ctx.Table.Columns[i]) + columnWidths[i] + "}")
                                .Aggregate((a, b) => $"{a}{ctx.ContentIndent}{b}") + ctx.ContentIndent + "|";
        
        var indentWide = 2 * ctx.Table.Options.ContentIndent;
        var contentAreas = columnWidths.Select(w => w + indentWide).ToList();
        var div = ctx.Indent;
        div += Enumerable.Range(0, ctx.Table.Columns.Count)
                         .Select(i => "+{" + i + "," + "-" + contentAreas[i] + "}")
                         .Aggregate((a, b) => $"{a}{b}") + "+";

        var args = contentAreas.Select(object (length) => new string('-', length)).ToArray();
        string divider = string.Format(div, args);

        if (ctx.ShouldShowTopBorder || (ctx.ShouldShowTitle || ctx.ShouldShowHeader))
        {
            ctx.OutputTo.WriteLine("R" + divider[1..]);
        }

        foreach (var row in ctx.Table.Rows.SkipLast(1))
        {
            ctx.OutputTo.WriteLine(lineFormat, row);
            if (ctx.ShouldShowRowSeparator)
            {
                ctx.OutputTo.WriteLine(divider);
            }
        }

        ctx.OutputTo.WriteLine(lineFormat, ctx.Table.Rows.Last());

        if (ctx.ShouldShowFooter)
        {
            ctx.OutputTo.WriteLine("F" + divider[1..]);
        }
        else
        {
            if (ctx.ShouldShowBottomBorder)
            {
                ctx.OutputTo.WriteLine(divider);
            }
        }
    }

    private void PrintFooter(PrintContext ctx)
    {
        var footerParts = ctx.Table.Footer.Select(part => part?.ToString()).ToList();
        var footerWidths = footerParts.Select(s => s?.Length ?? 0)
                                      .ToList();

        var footerDesiredWidth = this.GetFooterWidth(ctx.Table) - footerParts.First()!.Length;
        var footerFirstPartDesiredWidth = ctx.TableWidth - footerDesiredWidth;
        footerWidths[0] = footerFirstPartDesiredWidth;
        var lineFormat = ctx.Indent;
        var indices = Enumerable.Range(0, footerParts.Count);
        lineFormat += indices
                      .Select(i => "|" + ctx.ContentIndent + "{" + i + "," + (i == 0 ? "-" : "") + footerWidths[i] + "}")
                      .Aggregate((a, b) => $"{a}{ctx.ContentIndent}{b}") + ctx.ContentIndent + "|";

        var footerArea = ctx.TableWidth - 2;
        var divider = $"{ctx.Indent}+{new string('-', footerArea)}+";
        //if (!ctx.ShouldShowRowSeparator)
        //{
        //    ctx.OutputTo.WriteLine("F" + divider[1..]);
        //}
        ctx.OutputTo.WriteLine(lineFormat, ctx.Table.Footer.ToArray());

        if (ctx.ShouldShowBottomBorder)
        {
            ctx.OutputTo.WriteLine(divider);
        }
    }

    private PrintContext CreateContext(ITextTable table, TableLayout layout, TextWriter output)
    {
        var tableWidth = this.GetTableWidth(table);
        var columnLengths = this.GetColumnWidths(table).ToList();
        return PrintContext.Create(table, layout, output, columnLengths, tableWidth);
    }

    public void PrintTo(ITextTable table, TableLayout layout, TextWriter? output = null)
    {
        var ctx = CreateContext(table, layout, output);

        // print ruler
        if (table.Options.ShowRuler)
        {
            this.PrintRuler(ctx);
        }
        
        // print title
        if (ctx.ShouldShowTitle)
        {
            this.PrintTitle(ctx);
        }
        
        // print header
        if (ctx.ShouldShowHeader)
        {
            this.PrintHeader(ctx);
        }

        // print rows
        this.PrintRows(ctx);

        // print footer
        if (ctx.ShouldShowFooter)
        {
            this.PrintFooter(ctx);
        }
    }
}