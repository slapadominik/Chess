using System;
using Chess.API.Entity.Interfaces;
using Chess.Logic;
using Chess.Logic.Interfaces;

namespace Chess.API.Entity
{
    public class Table : ITable
    {
        public int Number { get; }
        public Guid PlayerBlackId { get; private set; }
        public Guid PlayerWhiteId { get; private set; }
        public IGame Game { get; set; }

        public Table(int number)
        {
            Number = number;
        }

        public void JoinTable(Guid playerId, Color color)
        {
            if (color == Color.White && PlayerWhiteId != default(Guid))
            {
                PlayerWhiteId = playerId;
            }
            else if (color == Color.Black && PlayerBlackId != default(Guid))
            {
                PlayerBlackId = playerId;
            }
        }

        public bool IsFull()
        {
            return PlayerBlackId != default(Guid) && PlayerWhiteId != default(Guid);
        }
    }
}