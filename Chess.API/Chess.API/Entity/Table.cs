using System;
using Chess.API.Entity.Interfaces;
using Chess.Logic;
using Chess.Logic.Consts;
using Chess.Logic.Interfaces;

namespace Chess.API.Entity
{
    public class Table : ITable
    {
        public int Number { get; }
        public IGame Game { get; set; }
        public Guid PlayerBlackId { get; private set; }
        public Guid PlayerWhiteId { get; private set; }

        public Table(int number)
        {
            Number = number;
        }

        public bool IsPlayer(Guid userId)
        {
            return PlayerWhiteId == userId || PlayerBlackId == userId;
        }

        public void JoinTable(Guid playerId, Color color)
        {
            if (color == Color.White && playerId != default(Guid))
            {
                PlayerWhiteId = playerId;
            }
            else if (color == Color.Black && playerId != default(Guid))
            {
                PlayerBlackId = playerId;
            }
        }

        public bool IsFull()
        {
            return PlayerBlackId != default(Guid) && PlayerWhiteId != default(Guid);
        }

        public bool PlayerBlackOccupied()
        {
            return PlayerBlackId == default(Guid);
        }

        public bool PlayerWhiteOccupied()
        {
            return PlayerWhiteId == default(Guid);
        }

        public void DismissPlayers()
        {
            PlayerBlackId = default(Guid);
            PlayerWhiteId = default(Guid);
        }
    }
}