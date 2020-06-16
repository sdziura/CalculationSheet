using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestFirstApp.UnitTests
{
    [TestClass]
    public class SheetTest5
    {

        Sheet sheet;
        TableModel model;  
        SheetFrame frame;       

        public void setUp()
        {
            sheet = new Sheet();

            model = new TableModel(sheet);
            frame = new SheetFrame(model);
        }

        [TestMethod]
        public void testThatFrameHasRightParts()
        {
            setUp();
            Assert.IsNotNull(frame.Table);
            Assert.IsNotNull(frame.Label);
            Assert.IsNotNull(frame.Editor);
            Assert.AreSame(model, frame.Table);
        }
       
        [TestMethod]
        public void testThatLabelIsUpdatedWhenSelectionChanges()
        {
            setUp();
            Assert.AreEqual("", frame.Label);

            frame.select(0, 1);
            Assert.AreEqual("A1", frame.Label);

            frame.select(10, 10);
            Assert.AreEqual("J11", frame.Label);
        }

        [TestMethod]
        public void testThatEditorSeesLiteralValue()
        {
            setUp();
            model.setValueAt("=7", 1, 1);
            frame.select(1, 1);

            Assert.AreEqual("=7", frame.Editor);
        }

        [TestMethod]
        public void testThatEditedValueGetsSaved()
        {
            setUp();
            model.setValueAt("=7", 1, 1);
            frame.select(1, 1);

            frame.Editor= "=8";
            frame.saveToCell();
            Assert.AreEqual("=8", frame.Table.getLiteralValueAt(1, 1));
            Assert.AreEqual("8", frame.Table.getValueAt(1, 1));
        }

        [TestMethod]
        public void testThatValuePropagationWorks()
        {
            setUp();
            frame.Table.setValueAt("7", 0, 1);
            frame.Table.setValueAt("=A1+2", 2, 2);
            Assert.AreEqual("9", frame.Table.getValueAt(2, 2));
            Assert.AreEqual("=A1+2", frame.Table.getLiteralValueAt(2, 2));

            frame.Table.setValueAt("10", 0, 1);
            Assert.AreEqual("12", frame.Table.getValueAt(2, 2));
        }

        [TestMethod]
        public void testAcceptanceTest1()
        {
            setUp();
            TableModel model;
            SheetFrame frame;

            model = new TableModel(new Sheet());
            frame = new SheetFrame(model);

            frame.select(0, 1);   // A1
            frame.Editor = "8";
            frame.saveToCell();

            frame.select(1, 1);   // A2
            frame.Editor = "=A1*A1+A1";
            frame.saveToCell();

            Assert.AreEqual("72", frame.Table.getValueAt(1, 1));

            frame.select(0, 1);   // A1
            frame.Editor = "5";
            frame.saveToCell();

            Assert.AreEqual("30", frame.Table.getValueAt(1, 1));
        }
    }
}

