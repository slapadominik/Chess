using System.Collections.Generic;
using System.ComponentModel;
using Chess.Logic.Consts;
using Chess.Logic.Exceptions;
using Chess.Logic.Interfaces;

namespace Chess.Logic.Figures
{
    public class King : Chessman
    {
        private int[] _validMoves;

        public King(Color color, string currentLocation) : base(color, currentLocation)
        {
            _validMoves = new int[] {-9, -8, -7, -1, 1, 7, 8, 9};
        }

        public override MoveResult Move(IBoard board, string to)
        {
            if (board.GetChessman(to) == null)
            {
                return MakeNonCaptureMove(board, CurrentLocation, to);
            }
            //if (IsInCheck() && validMoves.Count=0) = GameOver
            return MakeCaptureMove(board, CurrentLocation, to);
        }

        public override bool CanAttackField(IBoard board, string to)
        {          
            if (board.GetChessman(to)?.GetColor() == GetColor())
            {
                return false;
            }

            if (!CanMakeMoveToDestination(to))
            {
                return false;
            }

            return true;
        }

        public override IEnumerable<Move> GetPossibleMoves(IBoard board)
        {
            throw new System.NotImplementedException();
        }

        private MoveResult MakeNonCaptureMove(IBoard board, string from, string to)
        {
            if (!CanAttackField(board, to))
            {
                throw new InvalidMoveException($"{GetType()} cannot make move: {CurrentLocation}:{to}");
            }

            if (board.IsFieldAttacked(to, GetColor()))
            {
                throw new InvalidMoveException($"{GetType()} cannot make move: {CurrentLocation}:{to} - field is attacked");
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

            if (board.IsFieldAttacked(to, GetColor()))
            {
                throw new InvalidMoveException($"{GetType()} cannot make move: {CurrentLocation}:{to} - field is attacked");
            }

            CaptureChessman(board, to);
            MoveToDestination(board, to);

            return new MoveResult(from, to, MoveStatus.Capture, GetColor());
        }

        private bool CanMakeMoveToDestination(string field)
        {
            if (LocationToNumberMapper.ContainsKey(field))
            {
                var valueFrom = LocationToNumberMapper[CurrentLocation];
                var valueTo = LocationToNumberMapper[field];
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

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj is King)
            {
                return CurrentLocation.Equals(((King)obj).CurrentLocation);
            }

            return false;
        }

        public override string ToString()
        {
            switch (GetColor())
            {
                case Color.White:
                    return "w" + Figure.King;
                case Color.Black:
                    return "b" + Figure.King;
                default:
                    throw new InvalidEnumArgumentException();
            }
        }
    }
}