// -------------------------Copyright © 2024 M.Sukhanov. All rights reserved.-------------------------
//  <copyright file="ITextTable.cs" author="M.Sukhanov">
//      Copyright © 2024 M.Sukhanov. All rights reserved.
//  </copyright>
//  <date>16.12.2024</date>
// -------------------------Copyright © 2024 M.Sukhanov. All rights reserved.-------------------------

namespace Trustsoft.TextTables.Contracts;

/// <summary>
///   Represents text table of in-memory data.
/// </summary>
public interface ITextTable
{
    /// <summary>
    ///   The title of the table.
    /// </summary>
    string Title { get; }

    /// <summary>
    ///   The columns of the table.
    /// </summary>
    List<TableColumn> Columns { get; }

    /// <summary>
    ///   The collection of rows that belong to this table.
    /// </summary>
    IList<object?[]> Rows { get; }

    /// <summary>
    ///   Describes whether to indent table and how many.
    /// </summary>
    int Indent { get; set; }

    /// <summary>
    ///   Describes whether to indent column's content and how many.
    /// </summary>
    int ContentIndent { get; set; }

    /// <summary>
    ///   Describes whether to show table ruler.
    /// </summary>
    bool ShowRuler { get; set; }

    /// <summary>
    ///   Describes whether to show table title.
    /// </summary>
    bool ShowTitle { get; set; }

    /// <summary>
    ///   Describes whether to show table header.
    /// </summary>
    bool ShowHeader { get; set; }

    /// <summary>
    ///   The <see cref="TextWriter" /> to write table data to.
    /// </summary>
    TextWriter OutputTo { get; }

    /// <summary>
    ///   Writes table data to output defined by <see cref="P:OutputTo" />.
    /// </summary>
    void Write();

    /// <summary>
    ///   Writes table data to specified <paramref name="output" />.
    /// </summary>
    /// <param name="output"> The output to print out table data. </param>
    void WriteTo(TextWriter output);
}