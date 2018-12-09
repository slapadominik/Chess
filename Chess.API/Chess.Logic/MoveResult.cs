using System;
using Chess.Logic.Interfaces;

namespace Chess.Logic
{
    public class MoveResult
    {
        public string From { get; }
        public string To { get; }
        public MoveStatus MoveStatus { get; }
        public Color Color { get; }
        public string Captured { get; set; }

        public MoveResult(string from, string to, MoveStatus moveStatus, Color color)
        {
            From = from;
            To = to;
            MoveStatus = moveStatus;
            Color = color;
        }

        public MoveResult(string from, string to, MoveStatus moveStatus, Color color, string captured)
        {
            From = from;
            To = to;
            MoveStatus = moveStatus;
            Color = color;
            Captured = captured;
        }
    }
}