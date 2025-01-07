// -------------------------Copyright © 2024 M.Sukhanov. All rights reserved.-------------------------
//  <copyright file="TextTable.cs" author="M.Sukhanov">
//      Copyright © 2024 M.Sukhanov. All rights reserved.
//  </copyright>
//  <date>16.12.2024</date>
// -------------------------Copyright © 2024 M.Sukhanov. All rights reserved.-------------------------

namespace Trustsoft.TextTables;

using Trustsoft.TextTables.Contracts;

/// <summary>
///   Represents text table of in-memory data.
/// </summary>
/// <remarks> Implements the <see cref="ITextTable" />. </remarks>
public class TextTable : ITextTable
{
    /// <summary>
    ///   Gets or sets the title of the table.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    ///   Defines the columns of the table.
    /// </summary>
    public List<TableColumn> Columns { get; }

    /// <summary>
    ///   Gets the collection of rows that belong to this table.
    /// </summary>
    public IList<object?[]> Rows { get; } = [];
    
    /// <summary>
    ///   Gets the collection of footer data that belong to this table.
    /// </summary>
    public IList<object?> Footer { get; } = [];

    /// <summary>
    ///   Defines this table configuration options.
    /// </summary>
    public TableOptions Options { get; set; }

    private bool ShouldShowTitle => this.Options.ShowTitle && this.Title.Length > 0;
    
    private bool ShouldShowFooter => this.Options.ShowFooter && this.Footer.Count > 0;

    /// <summary>
    ///   Initializes a new instance of the <see cref="TextTable" /> class
    ///   with the specified <paramref name="columns" />.
    /// </summary>
    /// <param name="columns"> The table columns. </param>
    public TextTable(List<TableColumn> columns)
    {
        this.Options = new TableOptions();
        this.Columns = columns;
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="TextTable" /> class
    ///   with columns of specified <paramref name="columnNames">column names</paramref>.
    /// </summary>
    /// <param name="columnNames"> The column names. </param>
    public TextTable(List<string> columnNames)
            : this(columnNames.Select(name => new TableColumn(name)).ToList())
    {
        this.Options = new TableOptions();
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="TextTable" /> class
    ///   with the specified tuples of column name and content alignment.
    /// </summary>
    /// <param name="tuples"> The pairs of column name and content alignment. </param>
    public TextTable(List<(string name, Alignment alignment)> tuples)
            : this(tuples.Select(tuple => new TableColumn(tuple.name, tuple.alignment)).ToList())
    {
        this.Options = new TableOptions();
    }

    /// <summary>
    ///   Adds the row with specified <paramref name="values" /> as columns data.
    /// </summary>
    /// <param name="values"> The columns data. </param>
    /// <exception cref="ArgumentNullException"> If <paramref name="values" /> is <see langword="null"/>. </exception>
    /// <exception cref="Exception"> Column list is empty, set the columns first. </exception>
    /// <exception cref="Exception">
    ///   The number columns in the row does not match the values set width.
    /// </exception>
    public void AddRow(params object[] values)
    {
        ArgumentNullException.ThrowIfNull(values, nameof(values));

        if (!this.Columns.Any())
        {
            throw new Exception("Column list is empty, set the columns first");
        }

        if (this.Columns.Count != values.Length)
        {
            throw new Exception($"The number columns in the row ({this.Columns.Count}) " +
                                $"does not match the values 'width' ({values.Length})");
        }

        this.Rows.Add(values);
    }

    /// <summary>
    ///   Adds the footer with specified <paramref name="values" /> as data parts.
    /// </summary>
    /// <param name="values"> The footer data parts. </param>
    /// <exception cref="ArgumentNullException"> If <paramref name="values" /> is <see langword="null"/>. </exception>
    public void AddFooter(params object[] values)
    {
        ArgumentNullException.ThrowIfNull(values, nameof(values));
        
        values.ToList().ForEach(this.Footer.Add);
    }

    private IEnumerable<int> GetColumnWidths()
    {
        for (var columnIndex = 0; columnIndex < this.Columns.Count; columnIndex++)
        {
            var result = this.Rows
                             .Select(row => row[columnIndex])
                             .Select(rowValue => rowValue?.ToString() ?? string.Empty)
                             .Union([this.Columns[columnIndex].Name])
                             .Select(s => s.Length).Max();

            yield return result;
        }
    }

    private IEnumerable<int> GetFooterWidths()
    {
        return this.GetFooterWidths(this.Footer);
    }

    private IEnumerable<int> GetFooterWidths(IEnumerable<object?> parts)
    {
        return parts.Select(part => part?.ToString())
                    .Select(s => s?.Length ?? 0)
                    .ToList();
    }
    
    private int GetFooterWidth()
    {
        int width = 0;
        
        // add borders
        width += 2;
        
        // add column lengths
        width += this.GetFooterWidths().Sum();
        
        // add columns content indent left + right
        width += this.Footer.Count * (this.Options.ContentIndent * 2);
        
        // add column separators
        width += this.Footer.Count - 1;
        
        return width;
    }

    private int GetTableFullWidth()
    {
        int width = 0;
        
        // add borders
        width += 2;
        
        // add column lengths
        width += this.GetColumnWidths().Sum();
        
        // add columns content indent left + right
        width += this.Columns.Count * (this.Options.ContentIndent * 2);
        
        // add column separators
        width += this.Columns.Count - 1;
        
        return width;
    }

    /// <summary>
    ///   Writes this table data to output defined by <see cref="P:OutputTo" />.
    /// </summary>
    public void Write()
    {
        this.WriteTo(this.Options.OutputTo);
    }

    private void PrintRuler(TextWriter output, int fullTableWidth)
    {
        var tableIndent = new string(' ', this.Options.Indent);
        var s = tableIndent;

        for (var i = 0; i < fullTableWidth; i++)
        {
            var x = i + 1;
            var y = x % 10;

            s += y == 0 ? $"{x / 10}" : " ";
        }

        output.WriteLine(s);
        s = tableIndent;

        for (var i = 0; i < fullTableWidth; i++)
        {
            var x = i + 1;
            var y = x % 10;
            s += $"{y}";
        }

        output.WriteLine(s);
    }

    /// <summary>
    ///   Writes this table data to specified <paramref name="output" />.
    /// </summary>
    /// <param name="output"> The output to print out table data. </param>
    public void WriteTo(TextWriter output)
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

        var tableWidth = this.GetTableFullWidth();
        
        // print ruler
        if (this.Options.ShowRuler)
        {
            this.PrintRuler(output, tableWidth);
        }

        var tableIndent = new string(' ', this.Options.Indent);
        var contentIndent = new string(' ', this.Options.ContentIndent);

        string lineFormat;
        // print title
        if (this.ShouldShowTitle)
        {
            var titleArea = tableWidth - 2;
            lineFormat = $"{tableIndent}+{new string('-', titleArea)}+";
            output.WriteLine(lineFormat);
            var leftIndent = (titleArea - this.Title.Length) / 2;
            var leftPart = new string(' ', leftIndent);
            var rightPart = new string(' ', (titleArea - this.Title.Length) - leftIndent);
            lineFormat = $"{tableIndent}|{leftPart}{this.Title}{rightPart}|";
            output.WriteLine(lineFormat);
        }

        var widths = this.GetColumnWidths().ToList();

        var indentWide = 2 * this.Options.ContentIndent;
        var contentAreas = widths.Select(w => w + indentWide).ToList();
        var div = tableIndent;
        div += Enumerable.Range(0, this.Columns.Count)
                         .Select(i => "+{" + i + "," + "-" + contentAreas[i] + "}")
                         .Aggregate((a, b) => $"{a}{b}") + "+";

        var divider = string.Format(div, contentAreas.Select(object (length) => new string('-', length)).ToArray());

        // print table header
        if (this.Options.ShowHeader)
        {
            output.WriteLine(divider);
            lineFormat = tableIndent;
            lineFormat += Enumerable.Range(0, this.Columns.Count)
                                    .Select(i => "|" + contentIndent + "{" + i + "," +
                                                 GetAlignmentSpecifier(this.Columns[i]) + widths[i] + "}")
                                    .Aggregate((a, b) => $"{a}{contentIndent}{b}") + contentIndent + "|";
            // print column names
            output.WriteLine(lineFormat, this.Columns.Select(object (column) => column.Name).ToArray());
        }

        output.WriteLine(divider);

        // print table rows
        lineFormat = tableIndent;
        lineFormat += Enumerable.Range(0, this.Columns.Count)
                                .Select(i => "|" + contentIndent + "{" + i + "," +
                                             GetAlignmentSpecifier(this.Columns[i]) + widths[i] + "}")
                                .Aggregate((a, b) => $"{a}{contentIndent}{b}") + contentIndent + "|";

        foreach (var row in this.Rows)
        {
            output.WriteLine(lineFormat, row);
            output.WriteLine(divider);
        }
        
        // print footer
        if (this.ShouldShowFooter)
        {
            var footerParts = this.Footer.Select(part => part?.ToString()).ToList();
            var footerWidths = footerParts.Select(s => s?.Length ?? 0)
                                          .ToList();
            
            var footerDesiredWidth = this.GetFooterWidth() - footerParts.First()!.Length;
            var footerFirstPartDesiredWidth = tableWidth - footerDesiredWidth;
            footerWidths[0] = footerFirstPartDesiredWidth;
            lineFormat = tableIndent;
            var indices = Enumerable.Range(0, footerParts.Count);
            lineFormat += indices
                          .Select(i => "|" + contentIndent + "{" + i + "," + (i == 0 ? "-":"") + footerWidths[i] + "}")
                          .Aggregate((a, b) => $"{a}{contentIndent}{b}") + contentIndent + "|";
        
            output.WriteLine(lineFormat, this.Footer.ToArray());
            var footerArea = tableWidth - 2;
            lineFormat = $"{tableIndent}+{new string('-', footerArea)}+";
            output.WriteLine(lineFormat);
        }
    }
}