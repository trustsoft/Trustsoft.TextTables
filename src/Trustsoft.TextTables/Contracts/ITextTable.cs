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
    string Title { get; set; }

    /// <summary>
    ///   The collection of columns contained in this table.
    /// </summary>
    List<TableColumn> Columns { get; }

    /// <summary>
    ///   The collection of rows that belong to this table.
    /// </summary>
    /// <value> The rows data. </value>
    IList<object?[]> Rows { get; }
    
    /// <summary>
    ///   The collection of footer data that belong to this table.
    /// </summary>
    /// <value> The footer data. </value>
    IList<object?> Footer { get; }

    /// <summary>
    ///   The configuration options of the table.
    /// </summary>
    TableOptions Options { get; set; }

    /// <summary>
    ///   Writes table data to output defined by <see cref="P:OutputTo" />
    ///   and in specified output <paramref name="layout"/>.
    /// </summary>
    /// <param name="layout"> The output layout. </param>
    void Write(TableLayout layout = TableLayout.Default);

    /// <summary>
    ///   Writes table data to specified <paramref name="output" />
    ///   and in specified output <paramref name="layout"/>.
    /// </summary>
    /// <param name="output"> The output to print out table data. </param>
    /// <param name="layout"> The output layout. </param>
    void WriteTo(TextWriter output, TableLayout layout = TableLayout.Default);
}