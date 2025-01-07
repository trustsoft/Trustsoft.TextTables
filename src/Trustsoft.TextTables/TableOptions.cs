// -------------------------Copyright © 2024 M.Sukhanov. All rights reserved.-------------------------
//  <copyright file="TableOptions.cs" author="M.Sukhanov">
//      Copyright © 2024 M.Sukhanov. All rights reserved.
//  </copyright>
//  <date>22.12.2024</date>
// -------------------------Copyright © 2024 M.Sukhanov. All rights reserved.-------------------------

namespace Trustsoft.TextTables;

/// <summary>
///   Represents the <see cref="TextTable"/> possible configuration options.
/// </summary>
public class TableOptions
{
    /// <summary>
    ///   Describes whether to indent table and how many.
    /// </summary>
    /// <remarks> Defaults to 0. </remarks>
    public int Indent { get; set; } = 0;

    /// <summary>
    ///   Describes whether to indent column's content and how many.
    /// </summary>
    /// <remarks> Defaults to 1. </remarks>
    public int ContentIndent { get; set; } = 1;

    /// <summary>
    ///   Describes whether to show table ruler.
    /// </summary>
    /// <remarks> Defaults to <see langword="false"/>. </remarks>
    public bool ShowRuler { get; set; } = false;

    /// <summary>
    ///   Describes whether to show table title.
    /// </summary>
    /// <remarks> Defaults to <see langword="true"/>. </remarks>
    public bool ShowTitle { get; set; } = true;

    /// <summary>
    ///   Describes whether to show table header.
    /// </summary>
    /// <remarks> Defaults to <see langword="true"/>. </remarks>
    public bool ShowHeader { get; set; } = true;
    
    /// <summary>
    ///   Describes whether to show table footer.
    /// </summary>
    /// <remarks> Defaults to <see langword="true"/>. </remarks>
    public bool ShowFooter { get; set; } = true;

    /// <summary>
    ///   The <see cref="TextWriter" /> to write table data to.
    /// </summary>
    /// <remarks> Defaults to <see cref="Console.Out" />. </remarks>
    public TextWriter OutputTo { get; set; } = Console.Out;
}