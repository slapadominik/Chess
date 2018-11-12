using System;
using System.Collections.Generic;
using System.Text;

namespace Chess.Logic.Exceptions
{
    public class EmptyLocationException : Exception
    {
        public EmptyLocationException()
        {
        }

        public EmptyLocationException(string message) : base(message)
        {
        }
    }
}
