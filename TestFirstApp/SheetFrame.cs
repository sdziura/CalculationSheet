using System;
using System.Collections.Generic;
using System.Text;

namespace TestFirstApp
{
    public class SheetFrame
    {
        TableModel table;
        string label;
        string editor;
        
    
        public SheetFrame(TableModel _table)
        {
            table = _table;
            label = "";
            editor = "";
        }

        public TableModel Table { get => table; set => table = value; }
        public string Label { get => label; set => label = value; }
        public string Editor { get => editor; set => editor = value; }


       
        public void saveToCell()
        {
            Table.setValueAt(Editor, Table.SelectedRow, Table.SelectedColumn);
        }
                


        public void select(int row, int col)
        {
            Table.changeSelection(row, col);
            Label = Table.getAddress(Table.SelectedRow, Table.SelectedColumn);
            Editor = table.getLiteralValueAt(Table.SelectedRow, Table.SelectedColumn);
        }

        


    }
}
