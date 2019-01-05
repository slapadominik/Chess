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
            if (board.GetChessman(to)?.GetColor() == GetColor())
            {
                throw new InvalidMoveException($"Location [{to}] contains friendly chessman!");
            }

            if (!CanAttackField(board, to))
            {
                throw new InvalidMoveException($"{GetType()} cannot make move: {CurrentLocation}:{to}");
            }

            var from = CurrentLocation;
            var moveType = RecognizeMoveType(board, to);
            MoveToDestination(board, to);

            if (board.IsFieldAttacked(to, GetColor()))
            {
                MoveToDestination(board, from);
                if (moveType.status == MoveStatus.Capture)
                {
                    board.SetChessman(to, moveType.captured);
                    board.GetPlayerFigures(moveType.captured.GetColor()).Add(moveType.captured);
                }
                throw new InvalidMoveException($"{GetType()} cannot make move: {CurrentLocation}:{to} - field is attacked");
            }

            return new MoveResult(from, to, MoveStatus.Capture, GetColor(), moveType.captured?.ToString());
        }

        public override IEnumerable<Move> GetPossibleMoves(IBoard board)
        {
            throw new System.NotImplementedException();
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