using System;
using System.Collections.Generic;
using System.Text;

namespace TestFirstApp
{
    public class TableModel
    {
        Sheet sheet;
        int selectedRow;
        int selectedColumn;

        public int SelectedRow { get => selectedRow; set => selectedRow = value; }
        public int SelectedColumn { get => selectedColumn; set => selectedColumn = value; }

        public TableModel()
        {
            sheet = new Sheet();
        }

        public TableModel(Sheet _sheet)
        {
            sheet = _sheet;
        }

        public string getAddress(int row, int col)
        {
            string address = "";
            if (col == 0) return address;
            else address = getColumnName(col) + (row + 1).ToString();
            return address;

        }

        public string getColumnName(int col)
        {
            if (col == 0) return "";
            int rest = 0;
            int wholes = col ;
            string columnName = "";
            do
            {
                wholes--;
                rest = wholes % 26;
                columnName = (char)(rest + 65) + columnName;
                
            } while (0 != (wholes /= 26));

            return columnName;
        }

        public string getValueAt(int row, int col)
        {
            string address = "";
            if (col == 0) return (row + 1).ToString();
            else address = getColumnName(col) + (row + 1).ToString();

            return sheet.get(address);
        }

        public void setValueAt(string value, int row, int col)
        {
            string address = "";
            if (col == 0) return;
            else address = getColumnName(col) + (row + 1).ToString();

            sheet.put(address, value);
        }

        public string getLiteralValueAt(int row, int col)
        {
            string address = "";
            if (col == 0) return (row + 1).ToString();
            else address = getColumnName(col) + (row + 1).ToString();

            return sheet.getLiteral(address);
        }

        public void changeSelection(int row, int col)
        {
            SelectedRow = row;
            SelectedColumn = col;
        }
    }
}
