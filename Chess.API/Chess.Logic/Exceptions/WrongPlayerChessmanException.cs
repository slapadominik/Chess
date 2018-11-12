using System;

namespace Chess.Logic.Exceptions
{
    public class WrongPlayerChessmanException : Exception
    {
        public WrongPlayerChessmanException()
        {
        }

        public WrongPlayerChessmanException(string message) : base(message)
        {
        }
    }
}