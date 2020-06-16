using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestFirstApp.UnitTests
{
    [TestClass]
    public class SheetTest3
    {
        [TestMethod]
        public void testThatCellReferenceWorks()
        {
            Sheet sheet = new Sheet();
            sheet.put("A1", "8");
            sheet.put("A2", "=A1");
            Assert.AreEqual("8", sheet.get("A2"), "cell lookup");
        }

        [TestMethod]
        public void testThatCellChangesPropagate()
        {
            Sheet sheet = new Sheet();
            sheet.put("A1", "8");
            sheet.put("A2", "=A1");
            Assert.AreEqual("8", sheet.get("A2"), "cell lookup");

            sheet.put("A1", "9");
            Assert.AreEqual("9", sheet.get("A2"), "cell change propagation");
        }

        [TestMethod]
        public void testThatFormulasKnowCellsAndRecalculate()
        {
            Sheet sheet = new Sheet();
            sheet.put("A1", "8");
            sheet.put("A2", "3");
            sheet.put("B1", "=A1*(A1-A2)+A2/3");
            Assert.AreEqual("41", sheet.get("B1"), "calculation with cells");

            sheet.put("A2", "6");
            Assert.AreEqual("18", sheet.get("B1"), "re-calculation");
        }
        
        [TestMethod]
        public void testThatDeepPropagationWorks()
        {
            Sheet sheet = new Sheet();
            sheet.put("A1", "8");
            sheet.put("A2", "=A1");
            sheet.put("A3", "=A2");
            sheet.put("A4", "=A3");
            Assert.AreEqual("8", sheet.get("A4"), "deep propagation");

            sheet.put("A2", "6");
            Assert.AreEqual("6", sheet.get("A4"), "deep re-calculation");
        }

        [TestMethod]
        public void testThatFormulaWorksWithManyCells()
        {
            Sheet sheet = new Sheet();
            sheet.put("A1", "10");
            sheet.put("A2", "=A1+B1");
            sheet.put("A3", "=A2+B2");
            sheet.put("A4", "=A3");
            sheet.put("B1", "7");
            sheet.put("B2", "=A2");
            sheet.put("B3", "=A3-A2");
            sheet.put("B4", "=A4+B3");

            Assert.AreEqual("34", sheet.get("A4"), "multiple expressions - A4");
            Assert.AreEqual("51", sheet.get("B4"), "multiple expressions - B4");
        }

        [TestMethod]
        public void testThatCircularReferenceDoesntCrash()
        {
            Sheet sheet = new Sheet();
            sheet.put("A1", "=A1");
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void testThatCircularReferencesAdmitIt()
        {
            Sheet sheet = new Sheet();
            sheet.put("A1", "=A1");
            Assert.AreEqual("#Circular", sheet.get("A1"), "Detect circularity");
        }

    }
}
