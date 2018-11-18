using System;

namespace Chess.Logic.Exceptions
{
    public class InvalidFieldException : Exception
    {
        public InvalidFieldException()
        {
        }

        public InvalidFieldException(string message) : base(message)
        {
        }
    }
}