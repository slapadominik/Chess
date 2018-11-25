using System;

namespace Chess.Logic.Interfaces
{
    public interface IGame
    {
        Guid GetId();
        MoveStatus MakeMove(Guid playerId, string from, string to);
        int MovesCount();
        void StartGame(Guid userId);
        bool IsGameStarted();
    }
}