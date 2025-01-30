// -------------------------Copyright © 2024 M.Sukhanov. All rights reserved.-------------------------
//  <copyright file="Program.cs" author="M.Sukhanov">
//      Copyright © 2024 M.Sukhanov. All rights reserved.
//  </copyright>
//  <date>16.12.2024</date>
// -------------------------Copyright © 2024 M.Sukhanov. All rights reserved.-------------------------

namespace UsageSample.ConsoleApp;

using Trustsoft.TextTables;
using Trustsoft.TextTables.Contracts;

internal static class Program
{
    private static List<User> CreateUsers()
    {
        var users = new List<User>
        {
            new User("Cunningham", "John",
                     DateOnly.Parse("23.06.1988")),
            new User("Carter", "William Robert",
                     DateOnly.Parse("10.09.1985")),
            new User("Snowfields", "Mark",
                     DateOnly.Parse("16.05.1991"))
        };

        return users;
    }

    private static List<string> CreateStringColumns()
    {
        return
        [
            "First name",
            "Last name",
            "Birthday",
            "Age",
            "Email"
        ];
    }

    private static List<(string, Alignment)> CreateColumns()
    {
        return
        [
            ("First name", Alignment.Center),
            ("Last name", Alignment.Left),
            ("Birthday", Alignment.Left),
            ("Age", Alignment.Right),
            ("Email", Alignment.Right),
        ];
    }

    private static TextTable CreateTextTable1(List<User> users)
    {
        var columns = CreateStringColumns();

        var table = new TextTable(columns);
        foreach (var user in users)
        {
            table.AddRow(user.FirstName, user.LastName, user.Birthday, user.Age, user.Email);
        }
        table.AddFooter("Test footer content part 1", "part 2");
        table.Title = "User List";
        table.Options.ShowTitle = true;
        table.Options.ShowHeader = false;
        return table;
    }

    private static TextTable CreateTextTable2(List<User> users)
    {
        List<(string, Alignment)> columns = CreateColumns();

        var table = new TextTable(columns);
        table.Title = "User List";
        table.Options.ShowTitle = false;
        table.Options.ShowHeader = true;
        foreach (var user in users)
        {
            table.AddRow(user.FirstName, user.LastName, user.Birthday, user.Age, user.Email);
        }
        
        table.AddFooter("Test single part footer content");
        return table;
    }

    private static TextTable CreateTextTable3(List<User> users)
    {
        List<string> columns = CreateStringColumns();

        var table = new TextTable(columns);

        foreach (var user in users)
        {
            table.AddRow(user.FirstName, user.LastName, user.Birthday, user.Age, user.Email);
        }

        table.AddFooter("Test footer content part 1", "part 2");
        table.Title = "User List";
        table.Options.ShowTitle = false;
        table.Options.ShowHeader = false;
        return table;
    }

    private static TextTable CreateTextTable4(List<User> users)
    {
        var columns = CreateColumns();

        var table = new TextTable(columns);
        table.Title = "User List";
        table.Options.ShowTitle = false;
        table.Options.ShowHeader = true;
        table.Options.ShowFooter = false;

        foreach (var user in users)
        {
            table.AddRow(user.FirstName, user.LastName, user.Birthday, user.Age, user.Email);
        }

        table.AddFooter("Test footer content part 1", "part 2", "part 3");
        return table;
    }

    private static TextTable CreateTextTable5(List<User> users)
    {
        List<(string, Alignment)> columns = CreateColumns();

        var table = new TextTable(columns);
        table.Title = "User List";
        table.Options.ShowTitle = true;
        table.Options.ShowHeader = true;
        table.Options.ShowFooter = true;

        foreach (var user in users)
        {
            table.AddRow(user.FirstName, user.LastName, user.Birthday, user.Age, user.Email);
        }

        table.AddFooter("Test single part footer content");
        return table;
    }

    private static string GetMessage(ITextTable table, string sampleId, TableLayout layout)
    {
        var opts = table.Options;
        return $"TextTable sample #{sampleId} - Layout = {layout}" +
               Environment.NewLine +
               $"Show: Ruler = {opts.ShowRuler}, Title = {opts.ShowTitle}, Header = {opts.ShowHeader}, Footer = {opts.ShowFooter}, TopBottom = {layout != TableLayout.Minimal}";
    }

    private static void PrintTableLayouts(ITextTable table, TableLayout[] layouts, int id)
    {
        var count = 0;
        foreach (TableLayout layout in layouts)
        {
            Console.WriteLine(GetMessage(table, $"{id}.{++count}", layout));
            Console.WriteLine();
            table.ToConsole(layout);
            table.ToFile("1.txt");
            using var fs = new FileStream("2.txt", FileMode.OpenOrCreate);
            table.ToStream(fs);
            Console.WriteLine();
        }
    }

    static void Main()
    {
        TableLayout[] layouts = [TableLayout.Standard, TableLayout.Compact, TableLayout.Minimal];

        var users = CreateUsers();
        ITextTable table = CreateTextTable1(users);

        PrintTableLayouts(table, layouts, 1);

        table = CreateTextTable2(users);

        PrintTableLayouts(table, layouts, 2);

        table = CreateTextTable3(users);

        PrintTableLayouts(table, layouts, 3);

        table = CreateTextTable4(users);

        PrintTableLayouts(table, layouts, 4);

        table = CreateTextTable5(users);

        PrintTableLayouts(table, layouts, 5);

        Console.ReadLine();
    }
}