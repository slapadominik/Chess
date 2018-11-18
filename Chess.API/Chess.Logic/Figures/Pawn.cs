using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Chess.Logic.Exceptions;
using Chess.Logic.Helpers;
using Chess.Logic.Interfaces;

namespace Chess.Logic.Figures
{
    public class Pawn : Chessman
    {
        private bool _firstMove;
        private readonly int[] _validNormalMoves;
        private readonly int[] _validFirstMoves;
        private readonly int[] _validCaptureMoves;

        public Pawn(Color color) : base(color)
        {
            _firstMove = true;
            _validNormalMoves = new int[] {8, -8};
            _validFirstMoves = new int[]{16, -16};
            _validCaptureMoves = new int[] {9, -9, 7, -7};
        }

        public override MoveResult MakeMove(IBoard board, string from, string to)
        {
            if (board.GetChessman(to) == null)
            {
                if (_firstMove)
                {
                    ValidateMove(board, from, to, _validNormalMoves.Concat(_validFirstMoves));
                    _firstMove = false;
                }
                else
                {
                    ValidateMove(board, from, to, _validNormalMoves);
                }
                Move(board, from, to);
                return new MoveResult(board, MoveStatus.Normal);
            }

            if (board.GetChessman(to).GetColor() == GetColor())
            {
                throw new InvalidMoveException($"Location [{to}] contains friendly chessman!");
            }
            //TODO: implementacja przypadku promocji pionka
            ValidateMove(board, from, to, _validCaptureMoves);
            Move(board, from, to);
            return new MoveResult(board, MoveStatus.Capture);
        }

        private void ValidateMove(IBoard board, string from, string to, IEnumerable<int> validMoves)
        {
           if (!board.IsMoveValid(FilterValidMoves(validMoves, GetColor()), from, to))
           {
             throw new InvalidMoveException($"Pawn cannot make move [{from}:{to}]");
           }
        }

        private IEnumerable<int> FilterValidMoves(IEnumerable<int> moves, Color color)
        {
            if (color == Color.Black)
            {
                return moves.Where(x => x > 0);
            }

            return moves.Where(x => x < 0);
        }

        private void Move(IBoard board, string from, string to)
        {
            board.SetChessman(to, this);
            board.SetChessman(from, null);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj);
        }
    }
}