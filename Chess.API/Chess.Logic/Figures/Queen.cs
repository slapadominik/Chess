﻿using Chess.Logic.Interfaces;

namespace Chess.Logic.Figures
{
    public class Queen : Chessman
    {
        public Queen(Color color) : base(color)
        {
        }

        public override MoveResult MakeMove(IBoard board, string @from, string to)
        {
            throw new System.NotImplementedException();
        }
    }
}