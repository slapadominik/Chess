using System;
using System.Collections.Generic;
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

        public Pawn(Color color, string currentLocation) : base(color, currentLocation)
        {
            IsFirstMove = true;
        }

        public override MoveResult Move(IBoard board, string to)
        {
            if (board.GetChessman(to)?.GetColor() == GetColor())
            {
                throw new InvalidMoveException($"Location [{to}] contains friendly chessman!");
            }

            //TODO: implementacja przypadku promocji pionka
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
                throw new InvalidMoveException($"{GetType()} cannot make move: {CurrentLocation}:{to} - move leaves friendly king in check");
            }

            IsFirstMove = false;

            return new MoveResult(from, to, moveType.status, GetColor(), moveType.captured);
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
            throw new NotImplementedException();
        }

        private bool IsMoveValid(IBoard board, string to)
        {
            if (LocationToNumberMapper.ContainsKey(to))
            {
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
    }
}