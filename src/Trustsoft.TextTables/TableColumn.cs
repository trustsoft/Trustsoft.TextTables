﻿// -------------------------Copyright © 2024 M.Sukhanov. All rights reserved.-------------------------
//  <copyright file="TableColumn.cs" author="M.Sukhanov">
//      Copyright © 2024 M.Sukhanov. All rights reserved.
//  </copyright>
//  <date>18.12.2024</date>
// -------------------------Copyright © 2024 M.Sukhanov. All rights reserved.-------------------------

namespace Trustsoft.TextTables;

public class TableColumn
{
    public string Name { get; set; }
    
    public Alignment Alignment { get; set; }

    public TableColumn(string name, Alignment alignment = Alignment.Left)
    {
        this.Name = name;
        this.Alignment = alignment;
    }
}