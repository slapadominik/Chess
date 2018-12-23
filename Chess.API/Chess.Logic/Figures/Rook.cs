using System;
using System.Collections.Generic;
using Chess.Logic.Exceptions;
using Chess.Logic.Interfaces;

namespace Chess.Logic.Figures
{
    public class Rook : Chessman
    {
        private const int UP_INDICATOR = -8;
        private const int DOWN_INDICATOR = 8;
        private const int RIGHT_INDICATOR = 1;
        private const int LEFT_INDICATOR = -1;
        private readonly int[] _validHorizontalMoves = new int[] {1, 2, 3, 4, 5, 6, 7};
        


        public Rook(Color color, string currentLocation) : base(color, currentLocation)
        {
        }

        public override MoveResult Move(IBoard board, string to)
        {
            if (board.GetChessman(to)?.GetColor() == GetColor())
            {
                throw new InvalidMoveException($"Location [{to}] contains friendly chessman!");
            }
            if (!IsMoveValid(board, to))
            {
                throw new InvalidMoveException($"{GetType()} cannot make move: {CurrentLocation}:{to}");
            }

            var from = CurrentLocation;
            var moveStatus = RecognizeMoveType(board, to);
            MoveToDestination(board, to);

            if (board.IsKingInCheck(GetColor()))
            {
                MoveToDestination(board, from);
                throw new InvalidMoveException($"{GetType()} cannot make move: {CurrentLocation}:{to} - move leaves friendly king in check");
            }

            return new MoveResult(from, to, moveStatus.status, GetColor(), moveStatus.captured);
        }


        public override bool CanAttackField(IBoard board, string to)
        {
            return IsMoveValid(board, to);
        }

        public override IEnumerable<Move> GetPossibleMoves()
        {
            throw new NotImplementedException();
        }

        private bool IsMoveValid(IBoard board, string to)
        {
            var directionIndicator = GetDirectionIndicator(to);
            if (directionIndicator == null)
            {
                return false;
            }

            if (directionIndicator == DOWN_INDICATOR || directionIndicator == UP_INDICATOR)
            {
                return !IsCollisionOnPath(board, to, directionIndicator.Value);
            }

            if (directionIndicator == LEFT_INDICATOR || directionIndicator == RIGHT_INDICATOR)
            {
                return CanMoveHorizontally(board, to, directionIndicator.Value);
            }

            throw new InvalidOperationException();
        }

        private bool CanMoveHorizontally(IBoard board, string to, int directionIndicator)
        {
            if (LocationToNumberMapper.ContainsKey(to))
            {
                return IsHorizontalMoveValid(to, _validHorizontalMoves) && !IsCollisionOnPath(board, to, directionIndicator);
            }

            return false;
        }

        private int? GetDirectionIndicator(string to)
        {
            if (LocationToNumberMapper.ContainsKey(to))
            {
                var valueFrom = LocationToNumberMapper[CurrentLocation];
                var valueTo = LocationToNumberMapper[to];
                var diff = valueTo - valueFrom;

                if (diff < 0 && diff % UP_INDICATOR == 0)
                {
                    return UP_INDICATOR;
                }
                if (diff > 0 && diff % DOWN_INDICATOR == 0)
                {
                    return DOWN_INDICATOR;
                }

                if (diff < 0 && CurrentLocation[1]==to[1])
                {
                    return LEFT_INDICATOR;
                }

                if (diff > 0 && CurrentLocation[1]==to[1])
                {
                    return RIGHT_INDICATOR;
                }

                return null;
            }
            throw new InvalidFieldException($"Destination field [{to}] doesn't exist!");
        }

        private bool IsHorizontalMoveValid(string to, IEnumerable<int> validMoves)
        {
            if (LocationToNumberMapper.ContainsKey(to))
            {
                var valueFrom = LocationToNumberMapper[CurrentLocation];
                var valueTo = LocationToNumberMapper[to];
                foreach (var validMove in validMoves)
                {
                    if (Math.Abs(valueTo - valueFrom) == validMove)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool IsCollisionOnPath(IBoard board, string to, int directionIndicator)
        {
            var valueFrom = LocationToNumberMapper[CurrentLocation];
            var valueTo = LocationToNumberMapper[to];
            var diff = valueTo - valueFrom;
            var crossingFieldsCount = diff / directionIndicator;

            for (int i = 0; i < crossingFieldsCount - 1; i++)
            {
                if (board.GetChessman(NumberToLocationMapper[valueFrom + directionIndicator]) != null)
                {
                    return true;
                }
                valueFrom += directionIndicator;
            }

            return false;
        }


        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj is Rook)
            {
                return CurrentLocation.Equals(((Rook)obj).CurrentLocation);
            }

            return false;
        }
    }
}