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
            new User("Caningham", "William",
                     DateOnly.Parse("10.09.1985")),
            new User("Snowfields", "Mark",
                     DateOnly.Parse("16.05.1991"))
        };

        return users;
    }

    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        
        var users = CreateUsers();
        
        var columns = new List<string>
        {
            "First name",
            "Last name",
            "Birthday",
            "Age",
            "Email"
        };
        var table = new TextTable(columns);

        foreach (var user in users)
        {
            table.AddRow(user.FirstName, user.LastName, user.Birthday, user.Age, user.Email);
        }

        table.Write();
        Console.ReadLine();
    }
}