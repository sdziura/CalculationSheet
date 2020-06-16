using System;
using System.Collections.Generic;
using System.IO;

namespace TestFirstApp
{
    public class Tokenizer
    {
        private StringReader _reader;
        private readonly IEnumerator<Token> _tokens;

        public IEnumerable<Token> Scan(string expression)
        {
            _reader = new StringReader(expression);

            var tokens = new List<Token>();
            while (_reader.Peek() != -1)
            {
                var c = (char)_reader.Peek();
                if (Char.IsWhiteSpace(c))
                {
                    _reader.Read();
                    continue;
                }

                if (Char.IsDigit(c))
                {
                    var nr = ParseNumber();
                    tokens.Add(new NumberConstantToken(nr));
                }
                else if (Char.IsLetter(c))
                {
                    var addr = ParseAddress();
                    tokens.Add(new AdressToken(addr));
                }
                else
                    switch (c)
                    {
                        case '(':
                            tokens.Add(new BracketsOpenToken());
                            _reader.Read();
                             break;
                        case ')':
                            tokens.Add(new BracketsCloseToken());
                            _reader.Read();
                            break;
                        case '-':
                            tokens.Add(new MinusToken());
                            _reader.Read();
                            break;
                        case '+':
                            tokens.Add(new PlusToken());
                            _reader.Read();
                            break;
                        case '*':
                            tokens.Add(new MultipleToken());
                            _reader.Read();
                            break;
                        case '/':
                            tokens.Add(new DivideToken());
                            _reader.Read();
                            break;
                        default:
                        throw new Exception("Unknown character in expression: " + c);
                    }
            }
            return tokens;
        }

        private int ParseNumber()
        {
            var digits = new List<int>();
            while (Char.IsDigit((char)_reader.Peek()))
            {
                var digit = (char)_reader.Read();
                int i;
                if (int.TryParse(Char.ToString(digit), out i))
                {
                    digits.Add(i);
                }
                else
                    throw new Exception("Could not parse integer number when parsing digit: " + digit);
            }

            var nr = 0;
            var mul = 1;
            digits.Reverse();
            digits.ForEach(d =>
            {
                nr += d * mul;
                mul *= 10;
            });

            return nr;
        }
        private string ParseAddress()
        {
            string address = "";
            while (Char.IsLetter((char)_reader.Peek()))
            {
                var letter = (char)_reader.Read();
                address += (letter); 
            }
            while (Char.IsDigit((char)_reader.Peek()))
            {
                var digit = (char)_reader.Read();
                address += (digit);
            }

            return address;
        }
    }
}
