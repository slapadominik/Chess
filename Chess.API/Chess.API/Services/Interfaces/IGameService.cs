using System;
using Chess.Logic;
using Chess.Logic.Interfaces;

namespace Chess.API.Services.Interfaces
{
    public interface IGameService
    {
        IGame GetGame(Guid gameId);
        void DeleteGames();
        void StartGame(Guid gameId, Guid userId);
        Guid CreateGame(int tableNumber, Guid userId);

    }
}