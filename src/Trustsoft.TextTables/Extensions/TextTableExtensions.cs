// -------------------------Copyright © 2025 M.Sukhanov. All rights reserved.-------------------------
//  <copyright file="TextTableExtensions.cs" author="M.Sukhanov">
//      Copyright © 2025 M.Sukhanov. All rights reserved.
//  </copyright>
//  <date>28.01.2025</date>
// -------------------------Copyright © 2025 M.Sukhanov. All rights reserved.-------------------------

namespace Trustsoft.TextTables;

using Trustsoft.TextTables.Contracts;

/// <summary>
///   Contains extension methods for <see cref="ITextTable" />.
/// </summary>
public static class TextTableExtensions
{
    /// <summary>
    ///   Writes table data to <see cref="Console.Out"/>
    ///   with specified output <paramref name="layout" />.
    /// </summary>
    /// <param name="table"> The table which data should be written out. </param>
    /// <param name="layout"> The output layout. </param>
    public static void ToConsole(this ITextTable table, TableLayout layout = TableLayout.Default)
    {
        table.WriteTo(Console.Out, layout);
    }

    /// <summary>
    ///   Writes table data to file with specified <paramref name="path" />
    ///   and output <paramref name="layout" />.
    /// </summary>
    /// <param name="table"> The table which data should be written out. </param>
    /// <param name="path"> The file path to write table data to. </param>
    /// <param name="layout"> The output layout. </param>
    public static void ToFile(this ITextTable table, string path, TableLayout layout = TableLayout.Default)
    {
        using var streamWriter = new StreamWriter(path);
        table.WriteTo(streamWriter, layout);
    }
    
    /// <summary>
    ///   Writes table data to specified <paramref name="stream" />
    ///   and output <paramref name="layout" />.
    /// </summary>
    /// <param name="table"> The table which data should be written out. </param>
    /// <param name="stream"> The stream to write table data to. </param>
    /// <param name="layout"> The output layout. </param>
    public static void ToStream(this ITextTable table, Stream stream, TableLayout layout = TableLayout.Default)
    {
        using var streamWriter = new StreamWriter(stream);
        table.WriteTo(streamWriter, layout);
    }
}