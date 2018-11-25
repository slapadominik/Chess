using System;

namespace Chess.API.Exceptions
{
    public class TableNotFullException : Exception
    {
        public TableNotFullException()
        {
        }

        public TableNotFullException(string message) : base(message)
        {
        }
    }
}