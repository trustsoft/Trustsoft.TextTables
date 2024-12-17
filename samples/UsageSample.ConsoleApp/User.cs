// -------------------------Copyright © 2024 M.Sukhanov. All rights reserved.-------------------------
//  <copyright file="User.cs" author="M.Sukhanov">
//      Copyright © 2024 M.Sukhanov. All rights reserved.
//  </copyright>
//  <date>16.12.2024</date>
// -------------------------Copyright © 2024 M.Sukhanov. All rights reserved.-------------------------

namespace UsageSample.ConsoleApp;

public class User
{
    public User(string lastName, string firstName, DateOnly birthday)
    {
        this.LastName = lastName;
        this.FirstName = firstName;
        this.Birthday = birthday;
    }

    public string FirstName { get; }

    public string LastName { get; }

    public DateOnly Birthday { get; }

    public string FullName => $"{this.FirstName} {this.LastName}";

    public int Age => DateTime.Now.Year - this.Birthday.Year;

    public string Email => $"{FirstName.ToLowerInvariant()[0]}.{LastName.ToLowerInvariant()}@gmail.com";
}