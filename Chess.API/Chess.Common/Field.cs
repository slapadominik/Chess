using System;
using System.Collections.Generic;
using System.Text;

namespace Chess.Common
{
    public class Field
    {
        private string _symbol;
        private Chessman _chessman;

        public Field(string symbol)
        {
            _symbol = symbol;
        }

        public Field(string symbol, Chessman chessman)
        {
            _symbol = symbol;
            _chessman = chessman;
        }


    }
}
