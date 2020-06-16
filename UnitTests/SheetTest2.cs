using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestFirstApp.UnitTests
{
    [TestClass]
    public class SheetTest2
    {
        [TestMethod]
        public void testFormulaSpec()
        {
            Sheet sheet = new Sheet();
            sheet.put("B1", " =7"); 
            Assert.AreEqual(" =7", sheet.get("B1"), "Not a formula");
            Assert.AreEqual(" =7", sheet.getLiteral("B1"), "Unchanged");
        }

        [TestMethod]
        public void testConstantFormula()
        {
            Sheet sheet = new Sheet();
            sheet.put("A1", "=7");
            Assert.AreEqual("=7", sheet.getLiteral("A1"), "Formula");
            Assert.AreEqual("7", sheet.get("A1"), "Value");
        }

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
        }
    }
}
