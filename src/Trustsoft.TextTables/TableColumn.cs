// -------------------------Copyright © 2024 M.Sukhanov. All rights reserved.-------------------------
//  <copyright file="TableColumn.cs" author="M.Sukhanov">
//      Copyright © 2024 M.Sukhanov. All rights reserved.
//  </copyright>
//  <date>18.12.2024</date>
// -------------------------Copyright © 2024 M.Sukhanov. All rights reserved.-------------------------

namespace Trustsoft.TextTables;

/// <summary>
///   Represents the column in TextTable.
/// </summary>
public class TableColumn
{
    public static Alignment DefaultAlignment = Alignment.Left;

    /// <summary>
    ///   Gets or sets the name for the column.
    /// </summary>
    /// <value> The name. </value>
    public string Name { get; set; }

    /// <summary>
    ///   Gets or sets the column content alignment.
    /// </summary>
    /// <value> The content alignment of this column. </value>
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
    ///   with the specified <paramref name="name" /> and <paramref name="alignment" />.
    /// </summary>
    /// <param name="name"> The name of this column. </param>
    /// <param name="alignment"> The content alignment of this column. </param>
    public TableColumn(string name, Alignment alignment)
    {
        this.Name = name;
        this.Alignment = alignment;
    }
}