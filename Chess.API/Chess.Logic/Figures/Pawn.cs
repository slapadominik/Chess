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
            if (board.GetChessman(to)?.GetColor() == GetColor())
            {
                throw new InvalidMoveException($"Location [{to}] contains friendly chessman!");
            }

            if (board.GetChessman(to) == null)
            {
                return MakeNonCaptureMove(board, CurrentLocation, to);
            }

            return MakeCaptureMove(board, CurrentLocation, to);
        }

        public override bool CanAttackField(IBoard board, string to)
        {
            return IsMoveValid(to, FilterValidMoves(_validCaptureMoves, GetColor()));
        }

        private bool IsMoveValid(string to, IEnumerable<int> validMoves)
        {
            if (LocationToNumberMapper.ContainsKey(to))
            {
                var valueFrom = LocationToNumberMapper[CurrentLocation];
                var valueTo = LocationToNumberMapper[to];
                foreach (var validMove in validMoves)
                {
                    if (valueTo - valueFrom == validMove)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private MoveResult MakeNonCaptureMove(IBoard board, string from, string to)
        {
            if (IsFirstMove)
            {
                if (!IsMoveValid(to, FilterValidMoves(_validNormalMoves.Concat(_validFirstMoves), GetColor())))
                {
                    throw new InvalidMoveException($"{GetType()} cannot make move: {CurrentLocation}:{to}");
                }
            }
            else
            {
                if (!IsMoveValid(to, FilterValidMoves(_validNormalMoves, GetColor())))
                {
                    throw new InvalidMoveException($"{GetType()} cannot make move: {CurrentLocation}:{to}");
                }
            }
            
            MoveToDestination(board, to);
            IsFirstMove = false;
            return new MoveResult(from, to, MoveStatus.Normal, GetColor());
        }

        private MoveResult MakeCaptureMove(IBoard board, string from, string to)
        {
            //TODO: implementacja przypadku promocji pionka
            if (!CanAttackField(board, to))
            {
                throw new InvalidMoveException($"{GetType()} cannot make move: {CurrentLocation}:{to}");
            }
            MoveToDestination(board, to);
            IsFirstMove = false;
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