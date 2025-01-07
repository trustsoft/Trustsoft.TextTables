// -------------------------Copyright © 2024 M.Sukhanov. All rights reserved.-------------------------
//  <copyright file="TableColumn.cs" author="M.Sukhanov">
//      Copyright © 2024 M.Sukhanov. All rights reserved.
//  </copyright>
//  <date>18.12.2024</date>
// -------------------------Copyright © 2024 M.Sukhanov. All rights reserved.-------------------------

namespace Trustsoft.TextTables;

/// <summary>
///   Represents the column in <see cref="TextTable"/>.
/// </summary>
public class TableColumn
{
    /// <summary>
    ///   Defines default content alignment.
    /// </summary>
    /// <remarks> Defaults to <see cref="Alignment"/>. </remarks>
    public static Alignment DefaultAlignment = Alignment.Left;

    /// <summary>
    ///   Gets or sets the name for the column.
    /// </summary>
    /// <value> The column name. </value>
    public string Name { get; set; }

    /// <summary>
    ///   Gets or sets the column content alignment.
    /// </summary>
    /// <value> The column content alignment. </value>
    public Alignment Alignment { get; set; }

    /// <summary>
    ///   Initializes a new instance of the <see cref="TableColumn" /> class
    ///   with the specified name and left aligned content.
    /// </summary>
    /// <param name="name"> The name of this column. </param>
    public TableColumn(string name) : this(name, DefaultAlignment)
    {
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="TableColumn" /> class
    ///   with the specified <paramref name="name" /> and content <paramref name="alignment" />.
    /// </summary>
    /// <param name="name"> The column name. </param>
    /// <param name="alignment"> The column content alignment. </param>
    public TableColumn(string name, Alignment alignment)
    {
        this.Name = name;
        this.Alignment = alignment;
    }
}