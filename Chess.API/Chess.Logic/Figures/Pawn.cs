using System.Reflection;
using Chess.Logic.Exceptions;
using Chess.Logic.Helpers;
using Chess.Logic.Interfaces;

namespace Chess.Logic.Figures
{
    public class Pawn : Chessman
    {
        private bool _firstMove;
        private int[] _validMoves;
        private int[] _validFirstMoves;

        public Pawn(Color color) : base(color)
        {
            _firstMove = true;
            _validMoves = new int[] {8, -8};
            _validFirstMoves = new int[]{16, -16};
        }

        public override MoveResult MakeMove(IBoard board, string @from, string to)
        {
            if (_firstMove)
            {
                if (!MoveValidator.IsMoveValid(_validFirstMoves, @from, to))
                {
                    throw new InvalidMoveException($"Pawn cannot make move [{from}:{to}]"); 
                }
            }
            else
            {
                if (!MoveValidator.IsMoveValid(_validMoves, from, to))
                {
                    throw new InvalidMoveException($"Pawn cannot make move [{from}:{to}]");
                }
            }

            if (board.GetChessman(from) == null)
            {
                board.SetChessman(@to, this);
                board.SetChessman(@from, null);
                return new MoveResult(board, MoveStatus.Normal);
            }
            if (board.GetChessman(from).GetColor() == GetColor())
            {
                throw new InvalidMoveException($"Location [{to}] contains friendly chessman!");
            }
            //TODO: Implementacja przypadku, gdy jest figura przeciwnika

            return new MoveResult(board, MoveStatus.Capture);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj);
        }
    }
}