using System;

namespace Chess.Logic.Exceptions
{
    public class RemoveChessmanException : Exception
    {
        public RemoveChessmanException()
        {
        }

        public RemoveChessmanException(string message) : base(message)
        {
        }
    }
}