// -------------------------Copyright © 2025 M.Sukhanov. All rights reserved.-------------------------
//  <copyright file="TextTableTests.Helpers.cs" author="M.Sukhanov">
//      Copyright © 2025 M.Sukhanov. All rights reserved.
//  </copyright>
//  <date>04.02.2025</date>
// -------------------------Copyright © 2025 M.Sukhanov. All rights reserved.-------------------------

namespace Trustsoft.TextTables.Tests;

using System;
using System.Collections.Generic;

public sealed partial class TextTableTests
{
    private static List<string> CreateStringColumns()
    {
        return
        [
            "Name",
            "Value1",
            "Value2",
            "Value3",
        ];
    }

    private static List<(string, Alignment)> CreateColumns()
    {
        return
        [
            ("Name", Alignment.Left),
            ("Value1", Alignment.Center),
            ("Value2", Alignment.Right),
            ("Value3", Alignment.Right),
        ];
    }

    private static List<TestData> CreateTestData()
    {
        var users = new List<TestData>
        {
            new TestData("Name1",
                         10,
                         "2",
                         DateOnly.Parse("23.06.1988")),
            new TestData("Name2",
                         20,
                         "3",
                         DateOnly.Parse("10.09.1985")),
            new TestData("Name3",
                         30,
                         "4",
                         DateOnly.Parse("16.05.1991"))
        };

        return users;
    }

    private static TextTable CreateTable(List<string> columns, List<TestData> data)
    {
        var table = new TextTable(columns);

        foreach (var item in data)
        {
            table.AddRow(item.Name, item.Value1, item.Value2, item.Value3);
        }

        table.AddFooter("Footer part 1", "part 2");
        table.Title = "Data List";
        return table;
    }

    private static TextTable CreateTable(List<(string, Alignment)> columns, List<TestData> data)
    {
        var table = new TextTable(columns);

        foreach (var item in data)
        {
            table.AddRow(item.Name, item.Value1, item.Value2, item.Value3);
        }

        table.AddFooter("Footer part 1");
        table.Title = "Data List";
        return table;
    }

    private static TextTable CreateTitleHeaderFooterTable(int indent = 0, int contentIndent = 1)
    {
        var columns = CreateStringColumns();
        var data = CreateTestData();
        var table = CreateTable(columns, data);
        table.Options.Indent = indent;
        table.Options.ContentIndent = contentIndent;
        table.Options.ShowTitle = true;
        table.Options.ShowHeader = true;
        table.Options.ShowFooter = true;
        return table;
    }

    private static TextTable CreateRulerTitleHeaderTable(int indent = 0, int contentIndent = 1)
    {
        var columns = CreateStringColumns();
        List<TestData> data = CreateTestData();
        var table = CreateTable(columns, data);
        table.Options.Indent = indent;
        table.Options.ContentIndent = contentIndent;
        table.Options.ShowRuler = true;
        table.Options.ShowTitle = true;
        table.Options.ShowHeader = true;
        table.Options.ShowFooter = false;
        return table;
    }

    private static TextTable CreateTitleFooterTable(int indent = 0, int contentIndent = 1)
    {
        var columns = CreateStringColumns();
        List<TestData> data = CreateTestData();
        var table = CreateTable(columns, data);
        table.Options.Indent = indent;
        table.Options.ContentIndent = contentIndent;
        table.Options.ShowTitle = true;
        table.Options.ShowHeader = false;
        table.Options.ShowFooter = true;
        return table;
    }

    private static TextTable CreateHeaderFooterTable(int indent = 0, int contentIndent = 1)
    {
        var columns = CreateColumns();
        List<TestData> data = CreateTestData();
        var table = CreateTable(columns, data);
        table.Options.Indent = indent;
        table.Options.ContentIndent = contentIndent;
        table.Options.ShowTitle = false;
        table.Options.ShowHeader = true;
        table.Options.ShowFooter = true;
        return table;
    }
}