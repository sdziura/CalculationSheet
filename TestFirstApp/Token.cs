using System;
using System.Collections.Generic;
using System.Text;

namespace TestFirstApp
{
    public abstract class Token
    {
    }


    public class OperatorToken : Token
    {
    }
    public class PlusToken : OperatorToken
    {
    }

    public class MinusToken : OperatorToken
    {
    }

    public class MultipleToken : OperatorToken
    {
    }

    public class DivideToken : OperatorToken
    {
    }


    public class BracketsOpenToken : Token
    {
    }

    public class BracketsCloseToken : Token
    {
    }

    public class NumberConstantToken : Token
    {
        private readonly int _value;

        public NumberConstantToken(int value)
        {
            _value = value;
        }

        public int Value
        {
            get { return _value; }
        }
    }

    public class AdressToken : Token
    {
        private readonly string _value;

        public AdressToken(string value)
        {
            _value = value;
        }

        public string Value
        {
            get { return _value; }
        }
    }
}
