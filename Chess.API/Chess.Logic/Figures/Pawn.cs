using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Chess.Logic.Exceptions;
using Chess.Logic.Interfaces;

namespace Chess.Logic.Figures
{
    public class Pawn : Chessman
    {
        public bool IsFirstMove { get; set; }
        private readonly int[] _validNormalMoves;
        private readonly int[] _validFirstMoves;
        private readonly int[] _validCaptureMoves;

        public Pawn(Color color, string currentLocation) : base(color, currentLocation)
        {
            IsFirstMove = true;
            _validNormalMoves = new int[] {8, -8};
            _validFirstMoves = new int[]{16, -16};
            _validCaptureMoves = new int[] {9, -9, 7, -7};
        }

        public override MoveResult Move(IBoard board, string to)
        {
            if (board.GetChessman(to) == null)
            {
                return MakeNonCaptureMove(board, CurrentLocation, to);
            }

            return MakeCaptureMove(board, CurrentLocation, to);
        }

        private MoveResult MakeNonCaptureMove(IBoard board, string from, string to)
        {
            if (IsFirstMove)
            {
                ValidateMove(to, FilterValidMoves(_validNormalMoves.Concat(_validFirstMoves), GetColor()));
                IsFirstMove = false;
            }
            else
            {
                ValidateMove(to, FilterValidMoves(_validNormalMoves, GetColor()));
            }
            MoveToDestination(board, to);
            return new MoveResult(from, to, MoveStatus.Normal, GetColor());
        }

        private MoveResult MakeCaptureMove(IBoard board, string from, string to)
        {
            if (board.GetChessman(to).GetColor() == GetColor())
            {
                throw new InvalidMoveException($"Location [{to}] contains friendly chessman!");
            }
            //TODO: implementacja przypadku promocji pionka
            if (IsFirstMove)
            {
                ValidateMove(to, FilterValidMoves(_validCaptureMoves, GetColor()));
                IsFirstMove = false;
            }
            else
            {
                ValidateMove(to, FilterValidMoves(_validCaptureMoves, GetColor()));
            }

            MoveToDestination(board, to);
            return new MoveResult(from, to, MoveStatus.Capture, GetColor());
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