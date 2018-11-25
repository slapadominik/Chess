using System;

namespace Chess.API.Exceptions
{
    public class TableNotExistException : Exception
    {
        public TableNotExistException()
        {
        }

        public TableNotExistException(string message) : base(message)
        {
        }
    }
}