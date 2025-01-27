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
    ///   Gets or sets the title of this table.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    ///   Gets the collection of columns contained in this table.
    /// </summary>
    public IList<TableColumn> Columns { get; }

    /// <summary>
    ///   Gets the collection of rows that belong to this table.
    /// </summary>
    public IList<object?[]> Rows { get; } = [];
    
    /// <summary>
    ///   Gets the collection of footer data that belong to this table.
    /// </summary>
    public IList<object?> Footer { get; } = [];

    /// <summary>
    ///   Gets or sets the configuration options of this table.
    /// </summary>
    public TableOptions Options { get; set; }

    /// <summary>
    ///   Initializes a new instance of the <see cref="TextTable" /> class
    ///   with the specified <paramref name="options"/>,
    ///   <paramref name="columns" /> and <paramref name="title"/>.
    /// </summary>
    /// <param name="options"> The table configuration options. </param>
    /// <param name="columns"> The table columns. </param>
    /// <param name="title"> The table title. </param>
    public TextTable(TableOptions options, IList<TableColumn> columns, string title)
    {
        this.Options = options;
        this.Columns = columns;
        this.Title = title;
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="TextTable" /> class
    ///   with the specified <paramref name="columns" /> and <paramref name="title"/>.
    /// </summary>
    /// <param name="columns"> The table columns. </param>
    /// <param name="title"> The table title. </param>
    public TextTable(IList<TableColumn> columns, string title) : this(new TableOptions(), columns, title)
    {
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="TextTable" /> class
    ///   with the specified <paramref name="columns" />.
    /// </summary>
    /// <param name="columns"> The table columns. </param>
    public TextTable(IList<TableColumn> columns) : this(columns, string.Empty)
    {
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="TextTable" /> class
    ///   with the specified <paramref name="columns" />.
    /// </summary>
    /// <param name="columns"> The table columns. </param>
    public TextTable(IEnumerable<TableColumn> columns) : this(columns.ToList())
    {
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="TextTable" /> class
    ///   with columns of specified <paramref name="columnNames">column names</paramref>.
    /// </summary>
    /// <param name="columnNames"> The column names. </param>
    public TextTable(IList<string> columnNames) : this(columnNames.Select(name => new TableColumn(name)))
    {
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="TextTable" /> class
    ///   with columns of specified <paramref name="columnNames">column names</paramref>.
    /// </summary>
    /// <param name="columnNames"> The column names. </param>
    public TextTable(IEnumerable<string> columnNames) : this(columnNames.ToList())
    {
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="TextTable" /> class
    ///   with columns of specified <paramref name="columnNames">column names</paramref>.
    /// </summary>
    /// <param name="columnNames"> The column names. </param>
    public TextTable(params string[] columnNames) : this(columnNames.ToList())
    {
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="TextTable" /> class
    ///   with the specified <paramref name="tuples"/> of column name and content alignment.
    /// </summary>
    /// <param name="tuples"> The pairs of column name and content alignment. </param>
    public TextTable(IList<(string name, Alignment alignment)> tuples)
            : this(tuples.Select(tuple => new TableColumn(tuple.name, tuple.alignment)))
    {
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

    /// <summary>
    ///   Writes this table data to output defined by <see cref="P:OutputTo" />.
    /// </summary>
    /// <param name="layout"> The output layout. </param>
    public void Write(TableLayout layout = TableLayout.Default)
    {
        this.WriteTo(this.Options.OutputTo, layout);
    }

    /// <summary>
    ///   Writes this table data to specified <paramref name="output" />.
    /// </summary>
    /// <param name="output"> The output to print out table data. </param>
    /// <param name="layout"> The output layout. </param>
    public void WriteTo(TextWriter output, TableLayout layout = TableLayout.Default)
    {
        TextTablePrinter.PrintTo(output, this, layout);
    }
}