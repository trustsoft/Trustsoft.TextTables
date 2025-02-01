// -------------------------Copyright © 2025 M.Sukhanov. All rights reserved.-------------------------
//  <copyright file="StringExtensions.cs" author="M.Sukhanov">
//      Copyright © 2025 M.Sukhanov. All rights reserved.
//  </copyright>
//  <date>27.01.2025</date>
// -------------------------Copyright © 2025 M.Sukhanov. All rights reserved.-------------------------

namespace Trustsoft.TextTables;

internal static class StringExtensions
{
    /// <summary>
    ///   Returns a new string that center aligns the characters in a
    ///   string by padding them on the left and right with a specified
    ///   character, of a specified <paramref name="totalLength"/>.
    /// </summary>
    /// <param name="source"> The source string. </param>
    /// <param name="totalLength"> The number of characters to pad the source string. </param>
    /// <param name="paddingChar"> The padding character. </param>
    /// <returns>
    ///   The modified source string padded with as many <paramref name="paddingChar"/>
    ///   characters needed to create a length of <paramref name="totalLength"/>.
    /// </returns>
    public static string PadCenter(this string source, int totalLength, char paddingChar = ' ')
    {
        var spaces = totalLength - source.Length;
        var padLeft = spaces / 2 + source.Length;
        return source.PadLeft(padLeft, paddingChar).PadRight(totalLength, paddingChar);
    }
}