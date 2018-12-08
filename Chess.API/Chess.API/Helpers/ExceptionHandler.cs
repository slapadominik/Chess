using System;
using Chess.Logic.Exceptions;
using Microsoft.Extensions.Logging;

namespace Chess.API.Helpers
{
    public class ExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<ExceptionHandler> _logger;

        public ExceptionHandler(ILogger<ExceptionHandler> logger)
        {
            _logger = logger;
        }

        public void Handle(Exception exception)
        {
            if (IsDebug(exception))
            {
                _logger.LogDebug(exception.Message);
            }
            if (IsWarning(exception))
            {
                _logger.LogWarning(exception.Message);
            }
        }

        private bool IsDebug(Exception ex)
        {
            return ex is GameNotStartedException
                   || ex is NotACurrentPlayerException
                   || ex is EmptyLocationException
                   || ex is WrongPlayerChessmanException
                   || ex is InvalidMoveException;
        }

        private bool IsWarning(Exception ex)
        {
            return ex is InvalidFieldException;
        }
    }
}