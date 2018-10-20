using System;
using System.Diagnostics;

namespace Chess.Entity
{
    public class Game
    {
        public Guid Id { get; set; }
        public Player PlayerWhite { get; set; }
        public Player PlayerBlack { get; set; }
        public Board Board { get; set; }

        public TimeSpan TimeSpan { get; set; }
    }
}