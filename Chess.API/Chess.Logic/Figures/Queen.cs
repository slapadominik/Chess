using System;
using System.Collections.Generic;
using System.ComponentModel;
using Chess.Logic.Consts;
using Chess.Logic.Exceptions;
using Chess.Logic.Interfaces;

namespace Chess.Logic.Figures
{
    public class Queen : Chessman
    {
        private const int UP_INDICATOR = -8;
        private const int DOWN_INDICATOR = 8;
        private const int RIGHT_INDICATOR = 1;
        private const int LEFT_INDICATOR = -1;

        public Queen(Color color, string currentLocation) : base(color, currentLocation)
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
            var moveType = RecognizeMoveType(board, to);
            MoveToDestination(board, to);

            if (board.IsKingInCheck(GetColor()))
            {
                MoveToDestination(board, from);
                if (moveType.status == MoveStatus.Capture)
                {
                    board.SetChessman(to, moveType.captured);
                    board.GetPlayerFigures(moveType.captured.GetColor()).Add(moveType.captured);
                }
                throw new InvalidMoveException($"{GetType()} cannot make move: {CurrentLocation}:{to} - move leaves friendly king in check");
            }


            return new MoveResult(from, to, moveType.status, GetColor(), moveType.captured?.GetType().Name);
        }

        public override bool CanAttackField(IBoard board, string to)
        {
            if (to.Equals(CurrentLocation))
            {
                return false;
            }

            var horizontalVerticalDirectionIndicator = GetHorizontalVerticalDirectionIndicator(to);

            if (horizontalVerticalDirectionIndicator.HasValue)
            {
                return !IsCollisionOnHorizontalVerticalPath(board, to, horizontalVerticalDirectionIndicator.Value);
            }

            if (IsDiagonalMove(to))
            {
                return IsDiagonalMoveLegal(board, to);
            }

            return false;
        }

        public override IEnumerable<Move> GetPossibleMoves(IBoard board)
        {
            throw new NotImplementedException();
        }

        private bool IsMoveValid(IBoard board, string to)
        {
            if (typeof(King) == (board.GetChessmanType(to)))
            {
                return false;
            }

            var horizontalVerticalDirectionIndicator = GetHorizontalVerticalDirectionIndicator(to);

            if (horizontalVerticalDirectionIndicator.HasValue)
            {
                return !IsCollisionOnHorizontalVerticalPath(board, to, horizontalVerticalDirectionIndicator.Value);
            }

            if (IsDiagonalMove(to))
            {
                return IsDiagonalMoveLegal(board, to);
            }

            return false;
        }

        private bool IsDiagonalMove(string to)
        {
            return Math.Abs(CharToColumnNumberMapper[to[0]] - CharToColumnNumberMapper[CurrentLocation[0]]) == Math.Abs(to[1] - CurrentLocation[1]);
        }

        private bool IsDiagonalMoveLegal(IBoard board, string to)
        {
            var rowTo = (int) Char.GetNumericValue(to[1]);
            var column = CharToColumnNumberMapper[CurrentLocation[0]];
            var rowFrom = (int)Char.GetNumericValue(CurrentLocation[1]);
            var fieldsOnPath = Math.Abs(rowTo - rowFrom);
            var columnIndicator = (CharToColumnNumberMapper[to[0]] - column) / fieldsOnPath;
            var rowIndicator = (rowTo - rowFrom) / fieldsOnPath;

            for (int i = 0; i < fieldsOnPath-1; i++)
            {
                rowFrom += rowIndicator;
                column += columnIndicator;
                if (board.GetChessman(ColumnNumberToCharMapper[column] + (rowFrom).ToString()) != null)
                {
                    return false;
                }
            }

            return true;
        }

        private int? GetHorizontalVerticalDirectionIndicator(string to)
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

                if (diff < 0 && CurrentLocation[1] == to[1])
                {
                    return LEFT_INDICATOR;
                }

                if (diff > 0 && CurrentLocation[1] == to[1])
                {
                    return RIGHT_INDICATOR;
                }

                return null;
            }
            throw new InvalidFieldException($"Destination field [{to}] doesn't exist!");
        }

        private bool IsCollisionOnHorizontalVerticalPath(IBoard board, string to, int directionIndicator)
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

            if (obj is Queen)
            {
                return CurrentLocation.Equals(((Queen)obj).CurrentLocation);
            }

            return false;
        }

        public override string ToString()
        {
            switch (GetColor())
            {
                case Color.White:
                    return "w" + Figure.Queen;
                case Color.Black:
                    return "b" + Figure.Queen;
                default:
                    throw new InvalidEnumArgumentException();
            }
        }
    }
}