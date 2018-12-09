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
            if (board.GetChessman(to)?.GetColor() == GetColor())
            {
                throw new InvalidMoveException($"Location [{to}] contains friendly chessman!");
            }

            if (typeof(King) == (board.GetChessmanType(to)))
            {
                throw new InvalidMoveException($"{GetType().Name} cannot make move: {CurrentLocation}:{to} - there is opponent's king.");
            }

            var directionIndicator = GetDirectionIndicator(to);
            ValidatePath(board, to, directionIndicator);
            var moveType = RecognizeMoveType(board, to);
            var from = CurrentLocation;
            MoveToDestination(board, to);

            return new MoveResult(from, to, moveType.status, GetColor(), moveType.captured);
        }

        public (MoveStatus status, string captured) RecognizeMoveType(IBoard board, string to)
        {
            if (board.GetChessman(to) != null)
            {
                return (MoveStatus.Capture, board.GetChessmanType(to).Name);
            }

            return (MoveStatus.Normal,null);
        }

        private void ValidatePath(IBoard board, string to, int directionIndicator)
        {
            var valueFrom = LocationToNumberMapper[CurrentLocation];
            var valueTo = LocationToNumberMapper[to];
            var diff = valueTo - valueFrom;
            var crossingFieldsCount = diff / directionIndicator;

            for (int i = 0; i < crossingFieldsCount-1; i++)
            {
                if (board.GetChessman(NumberToLocationMapper[valueFrom + directionIndicator]) != null)
                {
                    throw new InvalidMoveException($"{GetType().Name} cannot make move: {CurrentLocation}:{to} - there are pieces on his path.");
                }
                valueFrom += directionIndicator;
            }
        }

        private int GetDirectionIndicator(string to)
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
                throw new InvalidMoveException($"{GetType()} cannot make move: {CurrentLocation}:{to}");
            }
            throw new InvalidFieldException($"Destination field [{to}] doesn't exist!");
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj);
        }

    }
}