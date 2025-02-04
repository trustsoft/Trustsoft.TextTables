// -------------------------Copyright © 2025 M.Sukhanov. All rights reserved.-------------------------
//  <copyright file="TextTableTests.cs" author="M.Sukhanov">
//      Copyright © 2025 M.Sukhanov. All rights reserved.
//  </copyright>
//  <date>02.02.2025</date>
// -------------------------Copyright © 2025 M.Sukhanov. All rights reserved.-------------------------

namespace Trustsoft.TextTables.Tests;

[TestClass]
public sealed partial class TextTableTests
{
    #region " Title, Header, Footer "

    [TestMethod]
    [TestCategory("Title, Header, Footer")]
    public void TestTableTitleHeaderFooterStandardLayout()
    {
        TextTable table = CreateTitleHeaderFooterTable();
        var actual = table.ToString(TableLayout.Standard);
        Assert.AreEqual("""
                        +--------------------------------------+
                        |              Data List               |
                        +-------+--------+--------+------------+
                        | Name  | Value1 | Value2 | Value3     |
                        +-------+--------+--------+------------+
                        | Name1 | 1      | 2      | 23.06.1988 |
                        +-------+--------+--------+------------+
                        | Name2 | 2      | 3      | 10.09.1985 |
                        +-------+--------+--------+------------+
                        | Name3 | 3      | 4      | 16.05.1991 |
                        +-------+--------+--------+------------+
                        | Test footer content part 1  | part 2 |
                        +--------------------------------------+
                        """,
                        actual);
    }

    [TestMethod]
    [TestCategory("Title, Header, Footer")]
    public void TestTableTitleHeaderFooterCompactLayout()
    {
        TextTable table = CreateTitleHeaderFooterTable();
        var actual = table.ToString(TableLayout.Compact);
        Assert.AreEqual("""
                        +--------------------------------------+
                        |              Data List               |
                        +-------+--------+--------+------------+
                        | Name  | Value1 | Value2 | Value3     |
                        +-------+--------+--------+------------+
                        | Name1 | 1      | 2      | 23.06.1988 |
                        | Name2 | 2      | 3      | 10.09.1985 |
                        | Name3 | 3      | 4      | 16.05.1991 |
                        +-------+--------+--------+------------+
                        | Test footer content part 1  | part 2 |
                        +--------------------------------------+
                        """,
                        actual);
    }

    [TestMethod]
    [TestCategory("Title, Header, Footer")]
    public void TestTableTitleHeaderFooterMinimalLayout()
    {
        TextTable table = CreateTitleHeaderFooterTable();
        var actual = table.ToString(TableLayout.Minimal);
        Assert.AreEqual("""
                        |              Data List               |
                        +-------+--------+--------+------------+
                        | Name  | Value1 | Value2 | Value3     |
                        +-------+--------+--------+------------+
                        | Name1 | 1      | 2      | 23.06.1988 |
                        | Name2 | 2      | 3      | 10.09.1985 |
                        | Name3 | 3      | 4      | 16.05.1991 |
                        +-------+--------+--------+------------+
                        | Test footer content part 1  | part 2 |
                        """,
                        actual);
    }

    #endregion

    #region " Ruler, Title, Header "

    [TestMethod]
    [TestCategory("Ruler, Title, Header")]
    public void TestTableTitleHeaderStandardLayout()
    {
        TextTable table = CreateRulerTitleHeaderTable();
        var actual = table.ToString(TableLayout.Standard);
        Assert.AreEqual("""
                                 1         2         3         4
                        1234567890123456789012345678901234567890
                        +--------------------------------------+
                        |              Data List               |
                        +-------+--------+--------+------------+
                        | Name  | Value1 | Value2 | Value3     |
                        +-------+--------+--------+------------+
                        | Name1 | 1      | 2      | 23.06.1988 |
                        +-------+--------+--------+------------+
                        | Name2 | 2      | 3      | 10.09.1985 |
                        +-------+--------+--------+------------+
                        | Name3 | 3      | 4      | 16.05.1991 |
                        +-------+--------+--------+------------+
                        """,
                        actual);
    }

    [TestMethod]
    [TestCategory("Ruler, Title, Header")]
    public void TestTableTitleHeaderCompactLayout()
    {
        TextTable table = CreateRulerTitleHeaderTable();
        var actual = table.ToString(TableLayout.Compact);
        Assert.AreEqual("""
                                 1         2         3         4
                        1234567890123456789012345678901234567890
                        +--------------------------------------+
                        |              Data List               |
                        +-------+--------+--------+------------+
                        | Name  | Value1 | Value2 | Value3     |
                        +-------+--------+--------+------------+
                        | Name1 | 1      | 2      | 23.06.1988 |
                        | Name2 | 2      | 3      | 10.09.1985 |
                        | Name3 | 3      | 4      | 16.05.1991 |
                        +-------+--------+--------+------------+
                        """,
                        actual);
    }

    [TestMethod]
    [TestCategory("Ruler, Title, Header")]
    public void TestTableTitleHeaderMinimalLayout()
    {
        TextTable table = CreateRulerTitleHeaderTable();
        var actual = table.ToString(TableLayout.Minimal);
        Assert.AreEqual("""
                                 1         2         3         4
                        1234567890123456789012345678901234567890
                        |              Data List               |
                        +-------+--------+--------+------------+
                        | Name  | Value1 | Value2 | Value3     |
                        +-------+--------+--------+------------+
                        | Name1 | 1      | 2      | 23.06.1988 |
                        | Name2 | 2      | 3      | 10.09.1985 |
                        | Name3 | 3      | 4      | 16.05.1991 |
                        """,
                        actual);
    }

    #endregion

    #region " Title, Footer "

    [TestMethod]
    [TestCategory("Title, Footer")]
    public void TestTableTitleFooterStandardLayout()
    {
        TextTable table = CreateTitleFooterTable();
        var actual = table.ToString(TableLayout.Standard);
        Assert.AreEqual("""
                        +--------------------------------------+
                        |              Data List               |
                        +-------+--------+--------+------------+
                        | Name1 | 1      | 2      | 23.06.1988 |
                        +-------+--------+--------+------------+
                        | Name2 | 2      | 3      | 10.09.1985 |
                        +-------+--------+--------+------------+
                        | Name3 | 3      | 4      | 16.05.1991 |
                        +-------+--------+--------+------------+
                        | Test footer content part 1  | part 2 |
                        +--------------------------------------+
                        """,
                        actual);
    }

    [TestMethod]
    [TestCategory("Title, Footer")]
    public void TestTableTitleFooterCompactLayout()
    {
        TextTable table = CreateTitleFooterTable();
        var actual = table.ToString(TableLayout.Compact);
        Assert.AreEqual("""
                        +--------------------------------------+
                        |              Data List               |
                        +-------+--------+--------+------------+
                        | Name1 | 1      | 2      | 23.06.1988 |
                        | Name2 | 2      | 3      | 10.09.1985 |
                        | Name3 | 3      | 4      | 16.05.1991 |
                        +-------+--------+--------+------------+
                        | Test footer content part 1  | part 2 |
                        +--------------------------------------+
                        """,
                        actual);
    }

    [TestMethod]
    [TestCategory("Title, Footer")]
    public void TestTableTitleFooterMinimalLayout()
    {
        TextTable table = CreateTitleFooterTable();
        var actual = table.ToString(TableLayout.Minimal);
        Assert.AreEqual("""
                        |              Data List               |
                        +-------+--------+--------+------------+
                        | Name1 | 1      | 2      | 23.06.1988 |
                        | Name2 | 2      | 3      | 10.09.1985 |
                        | Name3 | 3      | 4      | 16.05.1991 |
                        +-------+--------+--------+------------+
                        | Test footer content part 1  | part 2 |
                        """,
                        actual);
    }

    #endregion

    #region " Header, Footer "

    [TestMethod]
    [TestCategory("Header, Footer")]
    public void TestTableHeaderFooterStandardLayout()
    {
        TextTable table = CreateHeaderFooterTable();
        var actual = table.ToString(TableLayout.Standard);
        Assert.AreEqual("""
                        +-------+--------+--------+------------+
                        | Name  | Value1 | Value2 | Value3     |
                        +-------+--------+--------+------------+
                        | Name1 | 1      | 2      | 23.06.1988 |
                        +-------+--------+--------+------------+
                        | Name2 | 2      | 3      | 10.09.1985 |
                        +-------+--------+--------+------------+
                        | Name3 | 3      | 4      | 16.05.1991 |
                        +-------+--------+--------+------------+
                        | Test footer content part 1  | part 2 |
                        +--------------------------------------+
                        """,
                        actual);
    }

    [TestMethod]
    [TestCategory("Header, Footer")]
    public void TestTableHeaderFooterCompactLayout()
    {
        TextTable table = CreateHeaderFooterTable();
        var actual = table.ToString(TableLayout.Compact);
        Assert.AreEqual("""
                        +-------+--------+--------+------------+
                        | Name  | Value1 | Value2 | Value3     |
                        +-------+--------+--------+------------+
                        | Name1 | 1      | 2      | 23.06.1988 |
                        | Name2 | 2      | 3      | 10.09.1985 |
                        | Name3 | 3      | 4      | 16.05.1991 |
                        +-------+--------+--------+------------+
                        | Test footer content part 1  | part 2 |
                        +--------------------------------------+
                        """,
                        actual);
    }

    [TestMethod]
    [TestCategory("Header, Footer")]
    public void TestTableHeaderFooterMinimalLayout()
    {
        TextTable table = CreateHeaderFooterTable();
        var actual = table.ToString(TableLayout.Minimal);
        Assert.AreEqual("""
                        | Name  | Value1 | Value2 | Value3     |
                        +-------+--------+--------+------------+
                        | Name1 | 1      | 2      | 23.06.1988 |
                        | Name2 | 2      | 3      | 10.09.1985 |
                        | Name3 | 3      | 4      | 16.05.1991 |
                        +-------+--------+--------+------------+
                        | Test footer content part 1  | part 2 |
                        """,
                        actual);
    }

    #endregion
}