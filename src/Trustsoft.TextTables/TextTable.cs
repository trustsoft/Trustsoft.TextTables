// -------------------------Copyright © 2024 M.Sukhanov. All rights reserved.-------------------------
//  <copyright file="TextTable.cs" author="M.Sukhanov">
//      Copyright © 2024 M.Sukhanov. All rights reserved.
//  </copyright>
//  <date>16.12.2024</date>
// -------------------------Copyright © 2024 M.Sukhanov. All rights reserved.-------------------------

namespace Trustsoft.TextTables;

using Trustsoft.TextTables.Contracts;

public class TextTable : ITextTable
{
    public string Title { get; set; } = string.Empty;
    
    public List<TableColumn> Columns { get; }

    public IList<object?[]> Rows { get; } = [];
    
    public int Indent { get; set; } = 0;
    
    public int ContentIndent { get; set; } = 1;
    
    public bool ShowRuler { get; set; } = false;
    
    public bool ShowTitle { get; set; } = true;
    
    public bool ShowHeader { get; set; } = true;

    private bool ShouldShowTitle => this.ShowTitle && this.Title.Length > 0;

    public TextWriter OutputTo { get; set; } = Console.Out;

    public TextTable(List<TableColumn> columns)
    {
        this.Columns = columns;
    }
    
    public TextTable(List<string> columns)
        : this(columns.Select(name => new TableColumn(name)).ToList())
    {
    }

    public TextTable(List<(string name, Alignment alignment)> columns)
        : this(columns.Select(tuple => new TableColumn(tuple.name, tuple.alignment)).ToList())
    {
    }

    public void AddRow(params object[] values)
    {
        ArgumentNullException.ThrowIfNull(values);

        if (!this.Columns.Any())
        {
            throw new Exception("Column list is empty, set the columns first");
        }

        if (this.Columns.Count != values.Length)
        {
            throw new Exception($"The number columns in the row ({this.Columns.Count}) does not match the values 'width' ({values.Length})");
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
                                    .Select(i => "|" + contentIndent + "{" + i + "," + GetAlignmentSpecifier(this.Columns[i]) + widths[i] + "}")
                                    .Aggregate((a, b) => $"{a}{contentIndent}{b}") + contentIndent + "|";
            // print column names
            output.WriteLine(lineFormat, this.Columns.Select(object (column) => column.Name).ToArray());
        }
        
        output.WriteLine(divider);

        // print table rows
        lineFormat = tableIndent;
        lineFormat += Enumerable.Range(0, this.Columns.Count)
                                .Select(i => "|" + contentIndent + "{" + i + "," + GetAlignmentSpecifier(this.Columns[i]) + widths[i] + "}")
                                .Aggregate((a, b) => $"{a}{contentIndent}{b}") + contentIndent + "|";
        
        foreach (var row in this.Rows)
        {
            output.WriteLine(lineFormat, row);
            output.WriteLine(divider);
        }
    }
}