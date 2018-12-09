using System;

namespace Chess.Logic.Exceptions
{
    public class InvalidMoveException : Exception
    {
        public InvalidMoveException()
        {
        }

        public InvalidMoveException(string message) : base(message)
        {
        }
    }
}