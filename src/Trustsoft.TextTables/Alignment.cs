// -------------------------Copyright © 2024 M.Sukhanov. All rights reserved.-------------------------
//  <copyright file="Alignment.cs" author="M.Sukhanov">
//      Copyright © 2024 M.Sukhanov. All rights reserved.
//  </copyright>
//  <date>18.12.2024</date>
// -------------------------Copyright © 2024 M.Sukhanov. All rights reserved.-------------------------

namespace Trustsoft.TextTables;

/// <summary>
///   Specifies content alignment of <see cref="TextTable"/> <see cref="TableColumn">column</see>.
/// </summary>
public enum Alignment
{
    /// <summary>
    ///   Content is aligned to the left.
    /// </summary>
    Left = 0,

    /// <summary>
    ///   Content is aligned to the right.
    /// </summary>
    Right = 1,

    /// <summary>
    ///   Content is centered.
    /// </summary>
    Center = 2,
}