using System.Collections.Generic;
using System.Security.Cryptography;
using Chess.Logic.Exceptions;
using Chess.Logic.Interfaces;

namespace Chess.Logic
{
    public abstract class Chessman
    {
        private readonly Color _color;
        protected Dictionary<string, int> LocationMapper;

        public Chessman(Color color)
        {
            _color = color;
            
        }

        public Color GetColor()
        {
            return _color;
        }

        public abstract MoveStatus MakeMove(IBoard board, string @from, string @to);

        protected virtual void Move(IBoard board, string from, string to)
        {
            board.SetChessman(to, this);
            board.SetChessman(from, null);
        }

        protected virtual void ValidateMove(IBoard board, string from, string to, IEnumerable<int> validMoves)
        {
            if (!board.IsMoveValid(validMoves, from, to))
            {
                throw new InvalidMoveException($"{GetType().Name} cannot make move [{from}:{to}]");
            }
        }

    }
}