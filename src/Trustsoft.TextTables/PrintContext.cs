// -------------------------Copyright © 2025 M.Sukhanov. All rights reserved.-------------------------
//  <copyright file="PrintContext.cs" author="M.Sukhanov">
//      Copyright © 2025 M.Sukhanov. All rights reserved.
//  </copyright>
//  <date>18.01.2025</date>
// -------------------------Copyright © 2025 M.Sukhanov. All rights reserved.-------------------------

namespace Trustsoft.TextTables;

using Trustsoft.TextTables.Contracts;

internal class PrintContext
{
    public ITextTable Table { get; private set; }
    
    public bool ShouldShowTitle { get; private set; }
    
    public bool ShouldShowHeader { get; private set; }
    
    public bool ShouldShowFooter { get; private set; }
    
    public TableLayout Layout { get; set; }
    
    public TextWriter OutputTo { get; set; }
    
    public string Indent { get; private set; }
    
    public string ContentIndent { get; private set; }
    
    public int TableWidth { get; set; }
    
    public List<int> ColumnLengths { get; set; }
    
    public bool ShouldShowRowSeparator => this.Layout is TableLayout.Standard;

    public bool ShouldShowTopBorder => this.Layout != TableLayout.Minimal;

    public bool ShouldShowBottomBorder => this.Layout != TableLayout.Minimal;

    public static PrintContext Create(ITextTable table, TableLayout layout, TextWriter? output)
    {
        return new PrintContext
        {
            Table = table,
            Indent = new string(' ', table.Options.Indent),
            ContentIndent = new string(' ', table.Options.ContentIndent),
            ShouldShowTitle = table.Options.ShowTitle && table.Title.Length > 0,
            ShouldShowHeader = table.Options.ShowHeader,
            ShouldShowFooter = table.Options.ShowFooter && table.Footer.Count > 0,
            OutputTo = output ?? table.Options.OutputTo,
            Layout = layout,
        };
    }
}