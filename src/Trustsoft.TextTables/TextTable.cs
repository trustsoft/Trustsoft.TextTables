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
///   Implements the <see cref="ITextTable" />.
/// </summary>
/// <seealso cref="ITextTable" />
public class TextTable : ITextTable
{
    /// <summary>
    ///   Gets or sets the title of the table.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    ///   Determines the columns of the table.
    /// </summary>
    /// <value> The columns. </value>
    public List<TableColumn> Columns { get; }

    /// <summary>
    ///   Gets the collection of rows that belong to this table.
    /// </summary>
    /// <value> The rows. </value>
    public IList<object?[]> Rows { get; } = [];

    /// <summary>
    ///   Describes whether to indent table and how many.
    /// </summary>
    public int Indent { get; set; } = 0;

    /// <summary>
    ///   Describes whether to indent column's content and how many.
    /// </summary>
    public int ContentIndent { get; set; } = 1;

    /// <summary>
    ///   Describes whether to show table ruler.
    /// </summary>
    public bool ShowRuler { get; set; } = false;

    /// <summary>
    ///   Describes whether to show table title.
    /// </summary>
    public bool ShowTitle { get; set; } = true;

    /// <summary>
    ///   Describes whether to show table header.
    /// </summary>
    public bool ShowHeader { get; set; } = true;

    private bool ShouldShowTitle => this.ShowTitle && this.Title.Length > 0;

    /// <summary>
    ///   The <see cref="TextWriter" /> to write table data to.
    ///   Defaults to <see cref="Console.Out" />.
    /// </summary>
    public TextWriter OutputTo { get; set; } = Console.Out;

    /// <summary>
    ///   Initializes a new instance of the <see cref="TextTable" /> class
    ///   with the specified <paramref name="columns" />.
    /// </summary>
    /// <param name="columns"> The columns. </param>
    public TextTable(List<TableColumn> columns)
    {
        this.Columns = columns;
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="TextTable" /> class with the specified column names.
    /// </summary>
    /// <param name="columnNames"> The column names. </param>
    public TextTable(List<string> columnNames)
            : this(columnNames.Select(name => new TableColumn(name)).ToList())
    {
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="TextTable" /> class
    ///   with the specified tuples of column name and content alignment.
    /// </summary>
    /// <param name="tuples"> The pairs of column name and content alignment. </param>
    public TextTable(List<(string name, Alignment alignment)> tuples)
            : this(tuples.Select(tuple => new TableColumn(tuple.name, tuple.alignment)).ToList())
    {
    }

    /// <summary>
    ///   Adds the row with specified <paramref name="values" /> as columns data.
    /// </summary>
    /// <param name="values"> The values. </param>
    /// <exception cref="ArgumentNullException"> If <paramref name="values" /> is null. </exception>
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
            throw new Exception(
                    $"The number columns in the row ({this.Columns.Count}) does not match the values 'width' ({values.Length})");
        }

        this.Rows.Add(values);
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

    private int GetTableFullWidth()
    {
        int width = 0;
        
        // add borders
        width += 2;
        
        // add column lengths
        width += this.GetColumnWidths().Sum();
        
        // add columns content indent left + right
        width += this.Columns.Count * (this.ContentIndent * 2);
        
        // add column separators
        width += this.Columns.Count - 1;
        
        return width;
    }

    /// <summary>
    ///   Writes table data to output defined by <see cref="P:OutputTo" />.
    /// </summary>
    public void Write()
    {
        this.WriteTo(this.OutputTo);
    }

    private void PrintRuler(TextWriter output, int fullTableWidth)
    {
        output.WriteLine($"Full width: {fullTableWidth}");

        var tableIndent = new string(' ', this.Indent);
        var s = tableIndent;

        for (var i = 0; i < fullTableWidth; i++)
        {
            var x = i + 1;
            var y = x % 10;

            if (y == 0)
                s += $"{x / 10}";
            else
                s += $" ";
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
    ///   Writes table data to specified <paramref name="output" />.
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

        string lineFormat;
        // print ruler
        var tableWidth = this.GetTableFullWidth();

        if (this.ShowRuler)
        {
            this.PrintRuler(output, tableWidth);
        }

        var tableIndent = new string(' ', this.Indent);
        var contentIndent = new string(' ', this.ContentIndent);
        this.Title = "User List";

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

        var indentWide = 2 * this.ContentIndent;
        var contentAreas = widths.Select(w => w + indentWide).ToList();
        var div = tableIndent;
        div += Enumerable.Range(0, this.Columns.Count)
                         .Select(i => "+{" + i + "," + "-" + contentAreas[i] + "}")
                         .Aggregate((a, b) => $"{a}{b}") + "+";

        var divider = string.Format(div, contentAreas.Select(object (length) => new string('-', length)).ToArray());

        // print table header
        if (this.ShowHeader)
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
    }
}