using System;
using Chess.Logic.Consts;
using Chess.Logic.Interfaces;

namespace Chess.Logic
{
    public class MoveResult
    {
        public string Chess { get; set; }
        public string From { get; }
        public string To { get; }
        public MoveStatus MoveStatus { get; }
        public Color Color { get; }
        public string Captured { get; set; }

        public MoveResult(string chess,string from, string to, MoveStatus moveStatus, Color color)
        {
            Chess = chess;
            From = from;
            To = to;
            MoveStatus = moveStatus;
            Color = color;
        }

        public MoveResult(string chess, string from, string to, MoveStatus moveStatus, Color color, string captured)
        {
            Chess = chess;
            From = from;
            To = to;
            MoveStatus = moveStatus;
            Color = color;
            Captured = captured;
        }
    }
}