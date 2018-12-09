using System;

namespace Chess.API.Helpers
{
    public interface IExceptionHandler
    {
        void Handle(Exception exception);
    }
}