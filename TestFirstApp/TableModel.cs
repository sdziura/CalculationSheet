using System;
using System.Collections.Generic;
using System.Text;

namespace TestFirstApp
{
    public class TableModel
    {
        Sheet sheet;

        public TableModel()
        {
            sheet = new Sheet();
        }

        public TableModel(Sheet _sheet)
        {
            sheet = _sheet;
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
    }
}
