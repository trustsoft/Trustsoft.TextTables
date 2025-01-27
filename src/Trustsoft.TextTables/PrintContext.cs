// -------------------------Copyright © 2025 M.Sukhanov. All rights reserved.-------------------------
//  <copyright file="PrintContext.cs" author="M.Sukhanov">
//      Copyright © 2025 M.Sukhanov. All rights reserved.
//  </copyright>
//  <date>18.01.2025</date>
// -------------------------Copyright © 2025 M.Sukhanov. All rights reserved.-------------------------

namespace Trustsoft.TextTables;

using Trustsoft.TextTables.Contracts;

internal class PrintContext
{
    private const char SpaceChar = ' ';

    public List<int> ColumnWidths { get; set; }

    public string Indent { get; private set; }

    public int IndentSize { get; set; }

    public string ContentIndent { get; private set; }

    public int ContentIndentSize { get; set; }

    public TableLayout Layout { get; set; }

    public TextWriter OutputTo { get; set; }

    public bool ShouldShowBottomBorder => this.Layout != TableLayout.Minimal;

    public bool ShouldShowFooter { get; private set; }

    public bool ShouldShowHeader { get; private set; }

    public bool ShouldShowRowSeparator => this.Layout is TableLayout.Standard;

    public bool ShouldShowTitle { get; private set; }

    public bool ShouldShowTopBorder => this.Layout != TableLayout.Minimal;

    public ITextTable Table { get; private set; }

    public int TableWidth { get; set; }

    public PrintContext(ITextTable table, TextWriter outputTo, TableLayout layout,
                        List<int> columnWidths, int tableWidth)
    {
        this.ColumnWidths = columnWidths;
        this.ContentIndent = new string(SpaceChar, table.Options.ContentIndent);
        this.ContentIndentSize = table.Options.ContentIndent;
        this.Indent = new string(SpaceChar, table.Options.Indent);
        this.IndentSize = table.Options.Indent;
        this.Layout = layout;
        this.OutputTo = outputTo;
        this.ShouldShowFooter = table.Options.ShowFooter && table.Footer is { Count: > 0 };
        this.ShouldShowHeader = table.Options.ShowHeader && table.Columns is { Count: > 0 };
        this.ShouldShowTitle = table.Options.ShowTitle && !string.IsNullOrEmpty(table.Title);
        this.Table = table;
        this.TableWidth = tableWidth;
    }
}