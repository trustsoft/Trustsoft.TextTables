// -------------------------Copyright © 2025 M.Sukhanov. All rights reserved.-------------------------
//  <copyright file="TableLayout.cs" author="M.Sukhanov">
//      Copyright © 2025 M.Sukhanov. All rights reserved.
//  </copyright>
//  <date>18.01.2025</date>
// -------------------------Copyright © 2025 M.Sukhanov. All rights reserved.-------------------------

namespace Trustsoft.TextTables;

/// <summary>
///   Represents the <see cref="TextTable" /> possible output layout options.
/// </summary>
public enum TableLayout
{
    /// <summary>
    ///   Standard table layout.
    /// </summary>
    /// <remarks>
    ///   <![CDATA[
    ///    -------------------------------------------
    ///    | FirstName | LastName | Birthday   | Age |
    ///    -------------------------------------------
    ///    | First 1   | Last 1   | 08.06.1987 |  37 |
    ///    -------------------------------------------
    ///    | First 2   | Last 2   | 18.09.1996 |  28 |
    ///    -------------------------------------------
    ///    | First 3   | Last 3   | 21.04.2004 |  20 |
    ///    -------------------------------------------
    /// ]]>
    /// </remarks>
    Standard,

    /// <summary>
    ///   Compact table layout.
    /// </summary>
    /// <remarks>
    ///   <![CDATA[
    ///    -------------------------------------------
    ///    | FirstName | LastName | Birthday   | Age |
    ///    -------------------------------------------
    ///    | First 1   | Last 1   | 08.06.1987 |  37 |
    ///    | First 2   | Last 2   | 18.09.1996 |  28 |
    ///    | First 3   | Last 3   | 21.04.2004 |  20 |
    ///    -------------------------------------------
    /// ]]>
    /// </remarks>
    Compact,

    /// <summary>
    ///   Minimal table layout.
    /// </summary>
    /// <remarks>
    ///   <![CDATA[
    ///    | FirstName | LastName | Birthday   | Age |
    ///    -------------------------------------------
    ///    | First 1   | Last 1   | 08.06.1987 |  37 |
    ///    | First 2   | Last 2   | 18.09.1996 |  28 |
    ///    | First 3   | Last 3   | 21.04.2004 |  20 |
    /// ]]>
    /// </remarks>
    Minimal,

    /// <summary>
    ///   Default table layout.
    /// </summary>
    Default = Standard,
}