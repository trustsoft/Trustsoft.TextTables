// -------------------------Copyright © 2024 M.Sukhanov. All rights reserved.-------------------------
//  <copyright file="TableOptions.cs" author="M.Sukhanov">
//      Copyright © 2024 M.Sukhanov. All rights reserved.
//  </copyright>
//  <date>22.12.2024</date>
// -------------------------Copyright © 2024 M.Sukhanov. All rights reserved.-------------------------

namespace Trustsoft.TextTables;

/// <summary>
///   Represents the TextTable possible configuration options.
/// </summary>
public class TableOptions
{
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
    
    /// <summary>
    ///   Describes whether to show table footer.
    /// </summary>
    public bool ShowFooter { get; set; } = true;

    /// <summary>
    ///   The <see cref="TextWriter" /> to write table data to.
    ///   Defaults to <see cref="Console.Out" />.
    /// </summary>
    public TextWriter OutputTo { get; set; } = Console.Out;
}