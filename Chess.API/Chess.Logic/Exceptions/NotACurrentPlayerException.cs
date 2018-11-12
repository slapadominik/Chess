using System;

namespace Chess.Logic.Exceptions
{
    public class NotACurrentPlayerException : Exception
    {
        public NotACurrentPlayerException() { }
        public NotACurrentPlayerException(string msg) : base(msg) { }
    }
}