// -------------------------Copyright © 2025 M.Sukhanov. All rights reserved.-------------------------
//  <copyright file="TestData.cs" author="M.Sukhanov">
//      Copyright © 2025 M.Sukhanov. All rights reserved.
//  </copyright>
//  <date>03.02.2025</date>
// -------------------------Copyright © 2025 M.Sukhanov. All rights reserved.-------------------------

namespace Trustsoft.TextTables.Tests;

using System;

internal class TestData
{
    public TestData(string name, int value1, string value2, DateOnly value3)
    {
        this.Name = name;
        this.Value1 = value1;
        this.Value2 = value2;
        this.Value3 = value3;
    }

    public string Name { get; }
    
    public int Value1 { get; }
    
    public string Value2 { get; }
    
    public DateOnly Value3 { get; }
}