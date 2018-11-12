using System.Collections.Generic;
using System.Security.Cryptography;
using Chess.Logic.Interfaces;

namespace Chess.Logic
{
    public abstract class Chessman
    {
        private readonly Color _color;

        public Chessman(Color color)
        {
            _color = color;
        }

        public Color GetColor()
        {
            return _color;
        }

        public abstract IBoard MakeMove(IBoard board, string @from, string @to);

    }
}