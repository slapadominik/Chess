using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Chess.Logic.Consts;
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
            return IsMoveValid(board, to);
        }

        public override IEnumerable<Move> GetPossibleMoves(IBoard board)
        {
            List<Move> possibleDestinations = new List<Move>();
            var availableFields = GenerateHorizontalFields().Concat(GenerateVerticalFields());
            foreach (var field in availableFields)
            {
                if (IsMoveValid(board, field))
                {
                    possibleDestinations.Add(new Move(this, CurrentLocation, field));
                }
            }

            return possibleDestinations;
        }

        private bool IsMoveValid(IBoard board, string to)
        {
            if (board.GetChessman(to)?.GetColor() == GetColor())
            {
                return false;
            }

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

        private IEnumerable<string> GenerateHorizontalFields()
        {
            List<string> horizontalMoves = new List<string>();
            var directionNumber = 1;
            var currentLocationNumber = LocationToNumberMapper[CurrentLocation];
            while ( (currentLocationNumber - directionNumber) % 8 != 0)
            {
                horizontalMoves.Add(NumberToLocationMapper[currentLocationNumber-directionNumber]);
                directionNumber++;
            }

            directionNumber = 1;
            while ( (currentLocationNumber + directionNumber) % 8 != 1)
            {
                horizontalMoves.Add(NumberToLocationMapper[currentLocationNumber + directionNumber]);
                directionNumber++;
            }

            return horizontalMoves;
        }

        private IEnumerable<string> GenerateVerticalFields()
        {
            List<string> verticalFields = new List<string>();
            var directionNumber = 8;
            var currentLocationNumber = LocationToNumberMapper[CurrentLocation];
            while (LocationNumberExists(currentLocationNumber - directionNumber))
            {
                verticalFields.Add(NumberToLocationMapper[currentLocationNumber - directionNumber]);
                directionNumber += 8;
            }

            directionNumber = 8;
            while (LocationNumberExists(currentLocationNumber + directionNumber))
            {
                verticalFields.Add(NumberToLocationMapper[currentLocationNumber + directionNumber]);
                directionNumber += 8;
            }

            return verticalFields;
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

        public override string ToString()
        {
            switch (GetColor())
            {
                case Color.White:
                    return "w" + Figure.Rook;
                case Color.Black:
                    return "b" + Figure.Rook;
                default:
                    throw new InvalidEnumArgumentException();
            }
        }
    }
}