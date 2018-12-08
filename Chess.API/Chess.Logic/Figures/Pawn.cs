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
        public bool IsFirstMove { get; set; }
        private readonly int[] _validNormalMoves;
        private readonly int[] _validFirstMoves;
        private readonly int[] _validCaptureMoves;

        public Pawn(Color color) : base(color)
        {
            IsFirstMove = true;
            _validNormalMoves = new int[] {8, -8};
            _validFirstMoves = new int[]{16, -16};
            _validCaptureMoves = new int[] {9, -9, 7, -7};
        }

        public override MoveStatus MakeMove(IBoard board, string from, string to)
        {
            if (board.GetChessman(to) == null)
            {
                return MakeNonCaptureMove(board, from, to);
            }

            return MakeCaptureMove(board, from, to);
        }

        private MoveStatus MakeNonCaptureMove(IBoard board, string from, string to)
        {
            if (IsFirstMove)
            {
                ValidateMove(board, from, to, FilterValidMoves(_validNormalMoves.Concat(_validFirstMoves), GetColor()));
                IsFirstMove = false;
            }
            else
            {
                ValidateMove(board, from, to, FilterValidMoves(_validNormalMoves, GetColor()));
            }
            Move(board, from, to);
            return MoveStatus.Normal;
        }

        private MoveStatus MakeCaptureMove(IBoard board, string from, string to)
        {
            if (board.GetChessman(to).GetColor() == GetColor())
            {
                throw new InvalidMoveException($"Location [{to}] contains friendly chessman!");
            }
            //TODO: implementacja przypadku promocji pionka
            if (IsFirstMove)
            {
                ValidateMove(board, from, to, FilterValidMoves(_validCaptureMoves, GetColor()));
                IsFirstMove = false;
            }
            else
            {
                ValidateMove(board, from, to, FilterValidMoves(_validCaptureMoves, GetColor()));
            }

            Move(board, from, to);
            return MoveStatus.Capture;
        }

        private IEnumerable<int> FilterValidMoves(IEnumerable<int> moves, Color color)
        {
            if (color == Color.Black)
            {
                return moves.Where(x => x > 0);
            }

            return moves.Where(x => x < 0);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj);
        }
    }
}