using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestFirstApp.UnitTests
{
    [TestClass]
    public class SheetTest2
    {
        // Implement code for previous test before moving to next one.

        [TestMethod]
        public void testFormulaSpec()
        {
            Sheet sheet = new Sheet();
            sheet.put("B1", " =7"); // note leading space
            Assert.AreEqual(" =7", sheet.get("B1"), "Not a formula");
            Assert.AreEqual(" =7", sheet.getLiteral("B1"), "Unchanged");
        }

        // Next - start on parsing expressions

        [TestMethod]
        public void testConstantFormula()
        {
            Sheet sheet = new Sheet();
            sheet.put("A1", "=7");
            Assert.AreEqual("=7", sheet.getLiteral("A1"), "Formula");
            Assert.AreEqual("7", sheet.get("A1"), "Value");
        }

        // More formula tests. You may feel the need to make up 
        // additional intermediate test cases to drive your code
        // better. (For example, you might want to test "2*3" 

        // before "2*3*4".) That's fine, go ahead and create them.
        // Just keep moving one test at a time.

        // We're doing expressions; you may need to do a spike
        // (investigation) if you're not familiar with parsing.
        // For background, look up "recursive descent" or
        // "operator precedence". (Other techniques can work as well.)

        // Order of tests - I'm familiar enough with parsing to think
        // it's probably easiest to do them in this order (highest
        // precedence to lowest). For extra credit, you might redo 
        // this part of the exercise with the tests in a different order 
        // to see what difference it makes.

        [TestMethod]
        public void testParentheses()
        {
            Sheet sheet = new Sheet();
            sheet.put("A1", "=(7)");
            Assert.AreEqual("7", sheet.get("A1"), "Parends");
        }

        [TestMethod]
        public void testDeepParentheses()
        {
            Sheet sheet = new Sheet();
            sheet.put("A1", "=((((10))))");
            Assert.AreEqual("10", sheet.get("A1"), "Parends");
        }

        [TestMethod]
        public void testMultiply()
        {
            Sheet sheet = new Sheet();
            sheet.put("A1", "=2*3*4");
            Assert.AreEqual("24", sheet.get("A1"), "Times");
        }

        [TestMethod]
        public void testAdd()
        {
            Sheet sheet = new Sheet();
            sheet.put("A1", "=71+2+3");
            Assert.AreEqual("76", sheet.get("A1"), "Add");
        }

        [TestMethod]
        public void testPrecedence()
        {
            Sheet sheet = new Sheet();
            sheet.put("A1", "=7+2*3");
            Assert.AreEqual("13", sheet.get("A1"), "Precedence");
        }

        [TestMethod]
        public void testFullExpression()
        {
            Sheet sheet = new Sheet();
            sheet.put("A1", "=7*(2+3)*((((2+1))))");
            Assert.AreEqual("105", sheet.get("A1"), "Expr");
        }

        [TestMethod]
        public void testFullExpression2()
        {
            Sheet sheet = new Sheet();
            sheet.put("A1", "=7*(2+3)*((((2+1))))");
            Assert.AreEqual("105", sheet.get("A1"), "Expr");
        }


        // Add any test cases you feel are missing based on 
        // where your code is now.

        // Then try your hand at a few test cases: Add "-" and "/"

        // with normal precedence. 

        // Next, error handling.

        [TestMethod]
        public void testSimpleFormulaError()
        {
            Sheet sheet = new Sheet();
            sheet.put("A1", "=7*");
            Assert.AreEqual("#Error", sheet.get("A1"), "Error");
        }

        [TestMethod]
        public void testParenthesisError()
        {
            Sheet sheet = new Sheet();
            sheet.put("A1", "=(((((7))");
            Assert.AreEqual("#Error", sheet.get("A1"), "Error");
        }/*

        // Add any more error cases you need. Numeric errors (e.g.,
        // divide by 0) can return #Error too.

        // Take a deep breath and refactor. This was a big jump.
        // Next time we'll tackle formulas involving cells.
        */
    }
}
