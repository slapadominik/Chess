using System;

namespace Chess.Logic.Interfaces
{
    public interface IGameManager
    {
        Guid CreateGame(Guid playerWhiteId, Guid playerBlackId);
        void DeleteGames();
        IGame GetGame(Guid gameId);
    }
}