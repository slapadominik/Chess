using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Logic.Exceptions;
using Chess.Logic.Interfaces;

namespace Chess.Logic.Figures
{
    public class Bishop : Chessman
    {
        private IEnumerable<int> _validMovesTopLeft;
        private IEnumerable<int> _validMovesTopRight;
        private IEnumerable<int> _validMovesDownLeft;
        private IEnumerable<int> _validMovesDownRight;


        private const int TOP_LEFT_INDICATOR = -9;
        private const int TOP_RIGHT_INDICATOR = -7;
        private const int DOWN_LEFT_INDICATOR = 7;
        private const int DOWN_RIGHT_INDICATOR = 9;

        public Bishop(Color color, string currentLocation) : base(color, currentLocation)
        {
            _validMovesTopLeft = Enumerable.Range(1, 7).Select(a => a * TOP_LEFT_INDICATOR);
            _validMovesTopRight = Enumerable.Range(1, 7).Select(y => y * TOP_RIGHT_INDICATOR);
            _validMovesDownLeft = Enumerable.Range(1, 7).Select(z => z * DOWN_LEFT_INDICATOR);
            _validMovesDownRight = Enumerable.Range(1, 7).Select(a => a * DOWN_RIGHT_INDICATOR);
        }

        public override MoveResult Move(IBoard board, string to)
        {
            var from = CurrentLocation;
            if (!CanAttackField(board, to))
            {
                throw new InvalidMoveException($"{GetType()} cannot make move: {CurrentLocation}:{to}");
            }
            var moveType = RecognizeMoveType(board, to);      
            MoveToDestination(board, to);

            return new MoveResult(from, to, moveType.status, GetColor(), moveType.captured);
        }

        public override bool CanAttackField(IBoard board, string to)
        {
            if (board.GetChessman(to)?.GetColor() == GetColor())
            {
                return false;
            }

            if (typeof(King) == (board.GetChessmanType(to)))
            {
                return false;
            }

            var directionIndicator = GetDirectionIndicator(to);
            if (directionIndicator == null)
            {
                return false;
            }

            if (IsCollisionOnPath(board, to, directionIndicator.Value))
            {
                return false;
            }

            return true;
        }

        public (MoveStatus status, string captured) RecognizeMoveType(IBoard board, string to)
        {
            if (board.GetChessman(to) != null)
            {
                return (MoveStatus.Capture, board.GetChessmanType(to).Name);
            }

            return (MoveStatus.Normal,null);
        }

        private bool IsCollisionOnPath(IBoard board, string to, int directionIndicator)
        {
            var valueFrom = LocationToNumberMapper[CurrentLocation];
            var valueTo = LocationToNumberMapper[to];
            var diff = valueTo - valueFrom;
            var crossingFieldsCount = diff / directionIndicator;

            for (int i = 0; i < crossingFieldsCount-1; i++)
            {
                if (board.GetChessman(NumberToLocationMapper[valueFrom + directionIndicator]) != null)
                {
                    return true;
                }
                valueFrom += directionIndicator;
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

                if (diff < 0 && diff % TOP_LEFT_INDICATOR == 0)
                {
                    return TOP_LEFT_INDICATOR;
                }
                if (diff < 0 && diff % TOP_RIGHT_INDICATOR == 0)
                {
                    return TOP_RIGHT_INDICATOR;
                }
                if (diff > 0 && diff % DOWN_LEFT_INDICATOR == 0)
                {
                    return DOWN_LEFT_INDICATOR;
                }
                if (diff > 0 && diff % DOWN_RIGHT_INDICATOR == 0)
                {
                    return DOWN_RIGHT_INDICATOR;
                }

                return null;
            }
            throw new InvalidFieldException($"Destination field [{to}] doesn't exist!");
        }


        private bool IsCheck(IBoard board)
        {
            bool topLeftPath = IsKingPresentOnPath(board, TOP_LEFT_INDICATOR);
            bool topRightPath = IsKingPresentOnPath(board, TOP_RIGHT_INDICATOR);
            bool downLeftPath = IsKingPresentOnPath(board, DOWN_LEFT_INDICATOR);
            bool downRightPath = IsKingPresentOnPath(board, DOWN_RIGHT_INDICATOR);
            return topLeftPath || topRightPath || downLeftPath || downRightPath;
        }


        private bool IsKingPresentOnPath(IBoard board, int directionIndicator)
        {
            var valueFrom = LocationToNumberMapper[CurrentLocation];
            bool nearestPieceFound = false;
            Chessman nearestPiece = null;

            while (!nearestPieceFound)
            {
                if ((nearestPiece = board.GetChessman(NumberToLocationMapper[valueFrom + directionIndicator])) != null)
                {
                    nearestPieceFound = true;
                }

                valueFrom += directionIndicator;
            }

            return nearestPiece?.GetType() == typeof(King);
        }
        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj);
        }

    }
}