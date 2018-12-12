using System.Collections.Generic;
using System.Linq;
using Chess.Logic.Exceptions;
using Chess.Logic.Interfaces;

namespace Chess.Logic.Figures
{
    public class Knight : Chessman
    {
        private int[] _validMoves;

        public Knight(Color color, string currentLocation) : base(color, currentLocation)
        {
            _validMoves = new int[] {-17, 17, -15, 15, 10, -10, 6, -6};
        }

        public override MoveResult Move(IBoard board,string to)
        {
            if (board.GetChessman(to) == null)
            {
                return MakeNonCaptureMove(board, CurrentLocation, to);
            }

            return MakeCaptureMove(board, CurrentLocation, to);
        }

        public override bool CanAttackField(IBoard board, string to)
        {
            if (LocationToNumberMapper.ContainsKey(to))
            {
                var valueFrom = LocationToNumberMapper[CurrentLocation];
                var valueTo = LocationToNumberMapper[to];
                foreach (var validMove in _validMoves)
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
            if (!CanAttackField(board, to))
            {
                throw new InvalidMoveException($"{GetType()} cannot make move: {CurrentLocation}:{to}");
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

            if (!CanAttackField(board, to))
            {
                throw new InvalidMoveException($"{GetType()} cannot make move: {CurrentLocation}:{to}");
            }

            MoveToDestination(board, to);
            return new MoveResult(from, to, MoveStatus.Capture, GetColor());
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj);
        }
    }
}