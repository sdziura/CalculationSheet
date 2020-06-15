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


        // The following test is likely to pass already.
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

        // Refactor and get everything nice and clean.


        // Next: (I almost made this a separate part, and when I
        // originally did it, I did it in a different design session).
        // So take a break if you need one.

        // There's one big open issue for formulas: what about
        // circular references? 

        // I'll sketch some hints, but you should define your own tests
        // that drive toward a solution compatible with your own 
        // implementation.

        [TestMethod]
        public void testThatCircularReferenceDoesntCrash()
        {
            Sheet sheet = new Sheet();
            sheet.put("A1", "=A1");
            Assert.IsTrue(true);
        }

        // Just like errors return a special value, it might be nice
        // if circular references did too. (See notes below).

        [TestMethod]
        public void testThatCircularReferencesAdmitIt()
        {
            Sheet sheet = new Sheet();
            sheet.put("A1", "=A1");
            Assert.AreEqual("#Circular", sheet.get("A1"), "Detect circularity");
        }
       /* */
        // You might come up with some other approach that suits your 
        // taste. We won't be exploring this corner of the solution 
        // any further; you just want a scheme that blocks silly mistakes.
        // Make sure you test deep circularities involving partially
        // evaluated expressions.

        // A hint: if you blindly evaluate an expression you have no
        // control over how deep the expression can be, since
        // circular references appear to be infinitely deep.


        // Where are we? I intend to spend the next two parts hooking
        // up a GUI. Then there will be an optional part that pushes 
        // things in an unexpected direction just to get a sense
        // of our software's robustness.
    }
}
