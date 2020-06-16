﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestFirstApp.UnitTests
{
    [TestClass]
    public class SheetTest5
    {

        Sheet sheet;
        TableModel model;  // New for part 5
        SheetFrame frame;       // New for part 5

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
       /*
        [TestMethod]
        public void testThatRowAndColumnSelectionAllowed()
        {
            setUp();
            Assert.IsTrue(frame.Table.getRowSelectionAllowed());
            Assert.IsTrue((frame.Table.getColumnSelectionAllowed());
        } 

        
        public class TestSelectionListener : ListSelectionListener
        {
            
            public bool wasNotified = false;

            public TestSelectionListener() { }

            public void valueChanged(ListSelectionEvent e)
            {
                wasNotified = true;
            }
        }
        
        // I expect this test to pass; it verifies how I think listeners work.
        // You might call it a spike and omit it.

        [TestMethod]
        public void testThatSelectionsNotifyListeners()
        {
            setUp();
            TestSelectionListener listener = new TestSelectionListener();
            frame.table.getSelectionModel().addListSelectionListener(listener);

            Assert.IsTrue(!listener.wasNotified);

            frame.table.changeSelection(3, 2, false, false);

            Assert.IsTrue(listener.wasNotified);


            listener.wasNotified = false;
            frame.table.changeSelection(1, 1, false, false);
            Assert.IsTrue(listener.wasNotified);
        }*/
        
        // If you need info on hooking up a selection listener, see 
        // http://java.sun.com/docs/books/tutorial/uiswing/components/table.html#selection

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

        // You might add a main() routine to SheetFrame and
        // see how the GUI is looking.

        [TestMethod]
        public void testThatEditorSeesLiteralValue()
        {
            setUp();
            model.setValueAt("=7", 1, 1);
            frame.select(1, 1);

            Assert.AreEqual("=7", frame.Editor);
        }


        // We would like to have a way to programmatically let the 
        // text field click "Enter", but I don't see a mechanism.
        // So we'll use the okButton instead.

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


        // See discussion below on acceptance tests.

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
        }/**/
    }
}

