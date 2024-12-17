// -------------------------Copyright © 2024 M.Sukhanov. All rights reserved.-------------------------
//  <copyright file="ITextTable.cs" author="M.Sukhanov">
//      Copyright © 2024 M.Sukhanov. All rights reserved.
//  </copyright>
//  <date>16.12.2024</date>
// -------------------------Copyright © 2024 M.Sukhanov. All rights reserved.-------------------------

namespace Trustsoft.TextTables.Contracts;

public interface ITextTable
{
    List<string> Columns { get; }

    IList<object?[]> Rows { get; }
    
    TextWriter OutputTo { get; }

    void Write();
    
    void WriteTo(TextWriter output);
}