using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace TestFirstApp
{
    public class Sheet
    {
        Dictionary<string, string> sheet;

        public Sheet()
        {
            sheet = new Dictionary<string, string>();
        }

        public Sheet(Dictionary<string, string> _sheet)
        {
            sheet = new Dictionary<string, string> (_sheet);
        }

        private int getFormula(string formula, string address)
        {
            Tokenizer tokenizer = new Tokenizer();
            Parser parser = new Parser(tokenizer.Scan(Regex.Replace(formula, "=", "")), new Sheet(sheet), address);
            return parser.Parse();
        }

        public string getLiteral(string key)
        {
            string result;
            if (!sheet.TryGetValue(key, out result))
                return "";
            return sheet[key];   
        }

        public string get(string key)
        {
            string result;

            if (!sheet.TryGetValue(key, out result))
                return "";
            else if (Regex.IsMatch(result, @"^="))
                try {
                    return getFormula(result, key).ToString();
                }
                catch (ArgumentException e)
                {
                    return "#Circular";
                }
                catch (Exception e)
                {
                    return "#Error";
                }
            else if (Regex.IsMatch(result, @"^\s*\d+\s*$"))
                return Regex.Replace(result, " ", "");
            else
                return result;
        }

        public void put(string key, string value)
        {
            if (!sheet.ContainsKey(key))
                sheet.Add(key, value);
            else sheet[key] = value;
        }
    }
}
