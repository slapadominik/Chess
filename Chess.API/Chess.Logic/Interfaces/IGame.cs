using System;

namespace Chess.Logic.Interfaces
{
    public interface IGame
    {
        Guid GetId();
        void MakeMove(Guid id, string from, string to);
        int MovesCount();

    }
}