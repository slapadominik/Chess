using System;
using Chess.Logic;
using Chess.Logic.Interfaces;

namespace Chess.API.Services.Interfaces
{
    public interface IGameService
    {
        IGame GetGame(Guid gameId);
    }
}