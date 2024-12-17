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
    public List<string> Columns { get; }

    public IList<object?[]> Rows { get; } = [];
    
    public TextWriter OutputTo { get; set; } = Console.Out;
    
    public TextTable(List<string> columns)
    {
        this.Columns = columns;
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
                             .Union([this.Columns[columnIndex]])
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
        
        // add column content indent
        width += this.Columns.Count * 2;
        
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

        var s = string.Empty;

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
        s = string.Empty;
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
        // print ruler
        this.PrintRuler(output, this.GetTableFullWidth());
        
        // print table header
        var widths = this.GetColumnWidths().ToList();

        var div = Enumerable.Range(0, this.Columns.Count)
                            .Select(i => "+ {" + i + "," + "-" + widths[i] + "}")
                            .Aggregate((a, b) => $"{a} {b}") + " +";

        var divider = string.Format(div, widths.Select(object (length) => new string('-', length)).ToArray());

        Console.WriteLine(divider);

        var namesFormat = Enumerable.Range(0, this.Columns.Count)
                                    .Select(i => "| {" + i + "," + "-" + widths[i] + "}")
                                    .Aggregate((a, b) => $"{a} {b}") + " |";
        // print column names
        Console.WriteLine(namesFormat, this.Columns.Select(object (name) => name).ToArray());
        
        Console.WriteLine(divider);

        // print table rows
        namesFormat = Enumerable.Range(0, this.Columns.Count)
                                .Select(i => "| {" + i + "," + "-" + widths[i] + "}")
                                .Aggregate((a, b) => $"{a} {b}") + " |";
        
        foreach (var row in this.Rows)
        {
            Console.WriteLine(namesFormat, row);
            Console.WriteLine(divider);
        }
    }
}