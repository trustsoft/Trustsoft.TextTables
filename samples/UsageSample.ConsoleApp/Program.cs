// -------------------------Copyright © 2024 M.Sukhanov. All rights reserved.-------------------------
//  <copyright file="Program.cs" author="M.Sukhanov">
//      Copyright © 2024 M.Sukhanov. All rights reserved.
//  </copyright>
//  <date>16.12.2024</date>
// -------------------------Copyright © 2024 M.Sukhanov. All rights reserved.-------------------------

namespace UsageSample.ConsoleApp;

using Trustsoft.TextTables;

internal static class Program
{
    private static List<User> CreateUsers()
    {
        var users = new List<User>
        {
            new User("Carter", "John",
                     DateOnly.Parse("23.06.1988")),
            new User("Cunningham", "William Robert",
                     DateOnly.Parse("10.09.1985")),
            new User("Snowfields", "Mark",
                     DateOnly.Parse("16.05.1991"))
        };

        return users;
    }

    static void Main(string[] args)
    {
        var users = CreateUsers();
        
        List<string> columns =
        [
            "First name",
            "Last name",
            "Birthday",
            "Age",
            "Email"
        ];

        Console.WriteLine();
        Console.WriteLine("Text Table sample #1");
        var table = new TextTable(columns);
        foreach (var user in users)
        {
            table.AddRow(user.FirstName, user.LastName, user.Birthday, user.Age, user.Email);
        }
        table.AddFooter("Test footer content part 1", "part 2");
        table.Title = "User List";
        table.Write();
        
        List<(string, Alignment)> columns2 = 
        [
            ("First name", Alignment.Left),
            ("Last name", Alignment.Left),
            ("Birthday", Alignment.Left),
            ("Age", Alignment.Right),
            ("Email", Alignment.Right),
        ];

        Console.WriteLine();
        Console.WriteLine("Text Table sample #2");
        table = new TextTable(columns2);
        table.Title = "User List";
        foreach (var user in users)
        {
            table.AddRow(user.FirstName, user.LastName, user.Birthday, user.Age, user.Email);
        }
        
        table.AddFooter("Test footer content part 1", "part 2", "part 3");

        table.Write();
        
        Console.ReadLine();
    }
}