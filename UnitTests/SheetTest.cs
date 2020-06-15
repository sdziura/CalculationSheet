using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestFirstApp.UnitTests
{
    [TestClass]
    public class SheetTest
    {
        [TestMethod]
        public void testThatCellsAreEmptyByDefault()
        {
            Sheet sheet = new Sheet();
            Assert.AreEqual("", sheet.get("A1"));
            Assert.AreEqual("", sheet.get("ZX347"));
        }

        // Implement each test before going to the next one.
        
        [TestMethod]
        public void testThatTextCellsAreStored()
        {
            Sheet sheet = new Sheet();
            string theCell = "A21";

            sheet.put(theCell, "A string");
            Assert.AreEqual("A string", sheet.get(theCell));

            sheet.put(theCell, "A different string");
            Assert.AreEqual("A different string", sheet.get(theCell));

            sheet.put(theCell, "");
            Assert.AreEqual("", sheet.get(theCell));
        }

        // Implement each test before going to the next one; then refactor.

        [TestMethod]
        public void testThatManyCellsExist()
        {
            Sheet sheet = new Sheet();
            sheet.put("A1", "First");
            sheet.put("X27", "Second");
            sheet.put("ZX901", "Third");

            Assert.AreEqual("First", sheet.get("A1"), "A1");
            Assert.AreEqual("Second", sheet.get("X27"), "X27");
            Assert.AreEqual("Third", sheet.get("ZX901"), "ZX901");

            sheet.put("A1", "Fourth");
            Assert.AreEqual("Fourth", sheet.get("A1"), "A1 after");
            Assert.AreEqual("Second", sheet.get("X27"), "X27 same");
            Assert.AreEqual("Third", sheet.get("ZX901"), "ZX901 same");
        }


        // Implement each test before going to the next one.
        // You can split this test case if it helps.

        [TestMethod]
        public void testThatNumericCellsAreIdentifiedAndStored()
        {
            Sheet sheet = new Sheet();
            string theCell = "A21";

            sheet.put(theCell, "X99"); // "Obvious" string
            Assert.AreEqual("X99", sheet.get(theCell));

            sheet.put(theCell, "14"); // "Obvious" number
            Assert.AreEqual("14", sheet.get(theCell));

            sheet.put(theCell, " 99 X"); // Whole string must be numeric
            Assert.AreEqual(" 99 X", sheet.get(theCell));

            sheet.put(theCell, " 1234 "); // Blanks ignored
            Assert.AreEqual("1234", sheet.get(theCell));

            sheet.put(theCell, " "); // Just a blank
            Assert.AreEqual(" ", sheet.get(theCell));
        }

        // Refactor before going to each succeeding test.

        [TestMethod]
        public void testThatWeHaveAccessToCellLiteralValuesForEditing()
        {
            Sheet sheet = new Sheet();
            string theCell = "A21";

            sheet.put(theCell, "Some string");
            Assert.AreEqual("Some string", sheet.getLiteral(theCell));

            sheet.put(theCell, " 1234 ");
            Assert.AreEqual(" 1234 ", sheet.getLiteral(theCell));

            sheet.put(theCell, "=7"); // Foreshadowing formulas:)
            Assert.AreEqual("=7", sheet.getLiteral(theCell));
        }
        

        // We'll talk about "get" and formulas next time.
    }
}
