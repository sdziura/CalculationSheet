using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TestFirstApp
{

    // Sum          := Product { PlusMinus Sum }
    // PlusMinus    := "+" | "-"
    // Product      := Number { MultiDivide Product }
    // MultiDivide  := "*" | "/"
    // Expression   := Number | Adress | { "(" Sum ")" }
    // Number       := Digit { Digit }
    // Address      := Letter+ Digit+
    // Digit        := "0" | "1" | "2" | "3" | "4" | "5" | "6" | "7" | "8" | "9" 
    // Letter       := "A" | "B" | "C" | ...
    public class Parser
    {
        private readonly IEnumerator<Token> _tokens;
        private Sheet _sheet;
        private string _address;

        public Parser(IEnumerable<Token> tokens, Sheet sheet, string address)
        {
            _tokens = tokens.GetEnumerator();
            _sheet = sheet;
            _address = address;
        }

        public int Parse()
        {
            var result = 0;
            while (_tokens.MoveNext())
            {
                result = ParseSum();
            }

            return result;
        }


        private int ParseSum()
        {
            var first_product = ParseProduct();
            while (_tokens.Current is OperatorToken)
            {
                if (_tokens.Current is PlusToken)
                {
                    _tokens.MoveNext();
                     first_product += ParseProduct();
                }
                else if (_tokens.Current is MinusToken)
                {
                    _tokens.MoveNext();
                     first_product -= ParseProduct();
                }
                else
                    return first_product;
            }
            return first_product;
        }

        private int ParseProduct()
        {
            var first_expression = ParseExpression();
            while (_tokens.Current is OperatorToken)
            {
                if (_tokens.Current is MultipleToken)
                {
                    _tokens.MoveNext();
                    first_expression *= ParseExpression();
                }
                else if (_tokens.Current is DivideToken)
                {
                    _tokens.MoveNext();
                    first_expression /= ParseExpression();
                }
                else
                    return first_expression;      
            }
            return first_expression;
        }

        private int ParseExpression()
        {

            if (_tokens.Current is BracketsOpenToken)
            {
                _tokens.MoveNext();
                var result = ParseSum();
                var op = _tokens.Current;

                if (_tokens.Current is BracketsCloseToken)
                {
                    _tokens.MoveNext();
                    return result;
                }
                throw new Exception("Closing bracket missing after: " + op);
            }
            else if (_tokens.Current is NumberConstantToken)
            {
                return ParseNumber();
            }
            else if (_tokens.Current is AdressToken)
            {
                return ParseAddress();
            }

            throw new Exception("Illegal character found: " + _tokens.Current);
        }


        private int ParseNumber()
        {
            if (_tokens.Current is NumberConstantToken)
            {
                var number = (_tokens.Current as NumberConstantToken).Value;
                _tokens.MoveNext();
                return number;
            }

            throw new Exception("Expected a number constant but found " + _tokens.Current);
        }

        private int ParseAddress()
        {
            if (_tokens.Current is AdressToken)
            {
                var address = (_tokens.Current as AdressToken).Value;
                _tokens.MoveNext();

                if (address == _address)
                    throw new ArgumentException("Circular refference");
                var result = _sheet.get(address);
                if (Regex.IsMatch(result, @"^\s*\d+\s*$"))
                    return int.Parse(result);
                else if (result == "")
                    return 0;
                else
                    throw new Exception("Invalid data in field:" + result);
            }
            throw new Exception("Expected an address constant but found " + _tokens.Current);
        }
    }
}