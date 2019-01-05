using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Chess.Logic.Consts;
using Chess.Logic.Exceptions;
using Chess.Logic.Interfaces;

namespace Chess.Logic.Figures
{
    public class Pawn : Chessman
    {
        public bool IsFirstMove { get; set; }
        private const int FIRST_MOVE_DIFFERENCE = 16;
        private const int NORMAL_MOVE_DIFFERENCE = 8;
        private const int CAPTURE1_MOVE_DIFFERENCE = 9;
        private const int CAPTURE2_MOVE_DIFFERENCE = 7;
        private static readonly int[] _movesDifferences = new int[] {FIRST_MOVE_DIFFERENCE, NORMAL_MOVE_DIFFERENCE, CAPTURE1_MOVE_DIFFERENCE, CAPTURE2_MOVE_DIFFERENCE};
        

        public Pawn(Color color, string currentLocation) : base(color, currentLocation)
        {
            IsFirstMove = true;
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

            IsFirstMove = false;

            return new MoveResult(from, to, moveType.status, GetColor(), moveType.captured?.GetType().Name);
        }

        public override bool CanAttackField(IBoard board, string to)
        {
            if (LocationToNumberMapper.ContainsKey(to))
            {
                var locationDifference = LocationToNumberMapper[to]-LocationToNumberMapper[CurrentLocation];

                switch (Math.Abs(locationDifference))
                {
                    case CAPTURE1_MOVE_DIFFERENCE:
                    case CAPTURE2_MOVE_DIFFERENCE:
                        return GetColor() == Color.White ? locationDifference < 0 : locationDifference > 0;

                    default:
                        return false;
                }
            }

            return false;
        }

        public override IEnumerable<Move> GetPossibleMoves(IBoard board)
        {
            List<Move> possibleDestinations = new List<Move>();
            var currentLocationNumber = LocationToNumberMapper[CurrentLocation];
            var differences = GetColor() == Color.White ? _movesDifferences.Select(x => -x).ToArray() : _movesDifferences;

            foreach (var moveDifference in differences)
            {
                if (LocationNumberExists(currentLocationNumber + moveDifference))
                {
                    if (IsMoveValid(board, NumberToLocationMapper[currentLocationNumber + moveDifference]))
                    {
                        possibleDestinations.Add(new Move(this, CurrentLocation, NumberToLocationMapper[currentLocationNumber + moveDifference]));
                    }
                }
            }

            return possibleDestinations;
        }

        private bool IsMoveValid(IBoard board, string to)
        {
            if (LocationToNumberMapper.ContainsKey(to))
            {
                if (board.GetChessman(to)?.GetColor() == GetColor())
                {
                    return false;
                }

                var locationDifference = LocationToNumberMapper[to]-LocationToNumberMapper[CurrentLocation];

                switch (Math.Abs(locationDifference))
                {
                    case NORMAL_MOVE_DIFFERENCE:
                        if (board.GetChessman(to) != null)
                        {
                            return false;
                        }
                        return GetColor() == Color.White ? locationDifference < 0 : locationDifference > 0;
                    case CAPTURE1_MOVE_DIFFERENCE:
                    case CAPTURE2_MOVE_DIFFERENCE:
                        if (board.GetChessman(to) == null)
                        {
                            return false;
                        }
                        return board.GetChessman(to).GetColor() != GetColor() && GetColor() == Color.White ? locationDifference < 0 : locationDifference > 0;

                    case FIRST_MOVE_DIFFERENCE:
                        if (!IsFirstMove)
                        {
                            return false;
                        }
                        return GetColor() == Color.White ? locationDifference < 0 : locationDifference > 0  && IsPrecedingFieldEmpty(board, GetColor());

                    default:
                        return false;
                }
            }

            return false;            
        }        

        private bool IsPrecedingFieldEmpty(IBoard board, Color color)
        {
            var currentPositionNumber = LocationToNumberMapper[CurrentLocation];
            return board.GetChessman(NumberToLocationMapper[
                color == Color.White
                    ? currentPositionNumber - FIRST_MOVE_DIFFERENCE
                    : currentPositionNumber + FIRST_MOVE_DIFFERENCE]) == null;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj is Pawn)
            {
                return CurrentLocation.Equals(((Pawn) obj).CurrentLocation);
            }

            return false;
        }

        public override string ToString()
        {
            switch (GetColor())
            {
                case Color.White:
                    return "w" + Figure.Pawn;
                case Color.Black:
                    return "b" + Figure.Pawn;
                default:
                    throw new InvalidEnumArgumentException();
            }
        }
    }
}