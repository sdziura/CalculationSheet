using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestFirstApp.UnitTests
{
    [TestClass]
    public class SheetTest4
    {
        Sheet sheet;
        TableModel table;

        int LAST_COLUMN_INDEX = 49;
        int LAST_ROW_INDEX = 99;

        public void setUp()
        {
            sheet = new Sheet();
            table = new TableModel(sheet);
        }

        [TestMethod]
        public void testTableModelRequiredOverrides()
        {
            setUp();
            Assert.AreEqual("", table.getValueAt(10, 10));
        }

        [TestMethod]
        public void testColumnNames()
        {
            setUp();
            Assert.AreEqual("", table.getColumnName(0));
            Assert.AreEqual("A", table.getColumnName(1));
            Assert.AreEqual("Z", table.getColumnName(26));
            Assert.AreEqual("AW", table.getColumnName(49));
        }

        [TestMethod]
        public void testThatColumn0ContainsIndex()
        {
            setUp();
            Assert.AreEqual("1", table.getValueAt(0, 0));
            Assert.AreEqual("50", table.getValueAt(49, 0));
            Assert.AreEqual("100", table.getValueAt(99, 0));
        }

        [TestMethod]
        public void testThatMainColumnsHaveContents()
        {
            setUp();
            sheet.put("A1", "upper left");
            Assert.AreEqual("upper left", table.getValueAt(0, 1));

            sheet.put("A100", "lower left");
            Assert.AreEqual("lower left", table.getValueAt(LAST_ROW_INDEX, 1));

            sheet.put("AW1", "upper right");
            Assert.AreEqual("upper right", table.getValueAt(0, LAST_COLUMN_INDEX));

            sheet.put("AW100", "lower right");
            Assert.AreEqual("lower right", table.getValueAt(LAST_ROW_INDEX, LAST_COLUMN_INDEX));
        }


        [TestMethod]
        public void testThatStoresWorkThroughTableModel()
        {
            setUp();
            table.setValueAt("21", 0, 1);
            table.setValueAt("=A1", 1, 1);

            Assert.AreEqual("21", table.getValueAt(0, 1));
            Assert.AreEqual("21", table.getValueAt(1, 1));

            table.setValueAt("22", 0, 1);
            Assert.AreEqual("22", table.getValueAt(0, 1));
            Assert.AreEqual("22", table.getValueAt(1, 1));
        }

        [TestMethod]
        public void testThatSheetTableModelCanGetLiteral()
        {
            setUp();
            sheet.put("A1", "=7");
            String contents = table.getLiteralValueAt(0, 1);

            Assert.AreEqual("=7", contents);
        }
    }
}

