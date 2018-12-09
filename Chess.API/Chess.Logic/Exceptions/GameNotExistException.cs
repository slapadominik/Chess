using System;

namespace Chess.Logic.Exceptions
{
    public class GameNotExistException : Exception
    {
        public GameNotExistException()
        {
        }

        public GameNotExistException(string message) : base(message)
        {
        }
    }
}