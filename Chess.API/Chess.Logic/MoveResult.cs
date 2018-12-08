using System;
using Chess.Logic.Interfaces;

namespace Chess.Logic
{
    public class MoveResult
    {
        public string From { get; }
        public string To { get; }
        public MoveStatus MoveStatus { get; }
        public Guid CurrentPlayer { get; }

        public MoveResult(string from, string to, MoveStatus moveStatus, Guid currentPlayer)
        {
            From = from;
            To = to;
            MoveStatus = moveStatus;
            CurrentPlayer = currentPlayer;
        }
    }
}