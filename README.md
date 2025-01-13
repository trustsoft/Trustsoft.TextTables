![Logo](./docs/README_Banner.png)

Work with text tables with ease.

[GitHub repository](https://github.com/trustsoft/Trustsoft.TextTables "Visit GiHub Repository")\
![GitHub repo size](https://img.shields.io/github/repo-size/trustsoft/Trustsoft.TextTables?style=flat&logo=github&color=steelblue "Repository size")
![GitHub license](https://img.shields.io/github/license/trustsoft/Trustsoft.TextTables?style=flat&color=steelblue "Repository license")
![GitHub commit activity](https://img.shields.io/github/commit-activity/t/trustsoft/Trustsoft.TextTables?style=flat&color=steelblue "Total commits")

[![C#](https://img.shields.io/badge/C%23-gray?style=flat&logo=csharp)](https://dotnet.microsoft.com/en-us/languages/csharp)
[![NET 6.0 - 9.0](https://img.shields.io/badge/NET-6.0_--_9.0-steelblue?style=flat)](https://learn.microsoft.com/en-us/dotnet/fundamentals/)

[![NuGet Stable Version (Trustsoft.TextTables)](https://img.shields.io/nuget/v/Trustsoft.TextTables.svg?label=Stable&color=steelblue)](https://www.nuget.org/packages/Trustsoft.TextTables/latest)
[![NuGet Latest Version (Trustsoft.TextTables)](https://img.shields.io/nuget/vpre/Trustsoft.TextTables.svg?label=Latest&color=peru)](https://www.nuget.org/packages/Trustsoft.TextTables/absoluteLatest )
![NuGet Downloads](https://img.shields.io/nuget/dt/Trustsoft.TextTables?color=steelblue)


#### Features:
- Table indentation;
- Table title;
- Table footer;
- Table ruler;
- Column content indentation;
- Column content alignment (left, right).

### Usage:
```csharp
// --- Sample #1 ---
// Default table configuration options + Title and Footer.
var t = new TextTable(["#", "Name"]);
t.Title = "Title";
t.AddRow(1, "one");
t.AddRow(2, "two");
t.AddRow(3, "three");
t.AddFooter("Footer");
t.Write();
```
![sample #1 output](docs/sample1.png)
```csharp
// --- Sample #2 ---
// Default table configuration options + table indent = 4.
var t = new TextTable(["#", "Name"]);
t.Options.Indent = 4;
t.AddRow(1, "one");
t.AddRow(2, "two");
t.AddRow(3, "three");
t.Write();
```
![sample #2 output](docs/sample2.png)
```csharp
// --- Sample #3 ---
// Default table configuration options +  Title = "Sample Title", table indent = 4, ContentIndent = 3.
var t = new TextTable(["#", "Name"]);
t.Title = "Sample Title";
t.Options.Indent = 4;
t.Options.ContentIndent = 3;
t.AddRow(1, "one");
t.AddRow(2, "two");
t.AddRow(3, "three");
t.Write();
```
![sample #3 output](docs/sample3.png)