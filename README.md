# Trustsoft.TextTables
Work with text tables with ease.

#### Main features:
- Table indentation;
- Table title;
- Table footer;
- Table ruler;
- Column content alignment;
- Column content indentation.

Usage:
-----------------------------------------------------------------------------------

	// --- Sample #1 ---
	// Default table configuration options + Title and Footer.
	var t = new TextTable(["#", "Name"]);
	t.Title = "Title";
	t.AddRow(1, "one");
	t.AddRow(2, "two");
	t.AddRow(3, "three");
	t.AddFooter("Footer");
    t.Write();
![sample #1 output](docs/sample1.png)
	
	// --- Sample #2 ---
	// Default table configuration options + table indent = 4.
	var t = new TextTable(["#", "Name"]);
    t.Options.Indent = 4;
    t.AddRow(1, "one");
    t.AddRow(2, "two");
    t.AddRow(3, "three");
    t.Write();
![sample #2 output](docs/sample2.png)
	
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
![sample #3 output](docs/sample3.png)