using System;

namespace Chess.Common
{
    public class Player
    {
        public Guid Id { get; private set; }

        public Player(Guid id)
        {
            Id = id;
        }
    }
}