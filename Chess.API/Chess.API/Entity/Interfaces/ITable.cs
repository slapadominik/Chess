using System;
using System.Threading.Tasks;
using Chess.Logic;
using Chess.Logic.Interfaces;

namespace Chess.API.Entity.Interfaces
{
    public interface ITable
    {
        int Number { get; }
        Guid PlayerWhiteId { get; }
        Guid PlayerBlackId { get; }
        void JoinTable(Guid playerId, Color color);
        bool IsFull();
    }
}