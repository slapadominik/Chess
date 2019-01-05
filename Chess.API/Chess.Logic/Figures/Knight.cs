using System.Collections.Generic;
using System.Linq;
using Chess.Logic.Consts;
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
            if (board.GetChessman(to)?.GetColor() == GetColor())
            {
                throw new InvalidMoveException($"Location [{to}] contains friendly chessman!");
            }

            if (!IsMoveValid(to))
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

            return new MoveResult(from, to, moveType.status, GetColor(), moveType.captured);
        }

        public override bool CanAttackField(IBoard board, string to)
        {
            return IsMoveValid(to);
        }

        public override IEnumerable<Move> GetPossibleMoves(IBoard board)
        {
            throw new System.NotImplementedException();
        }

        private bool IsMoveValid(string to)
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


        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj is Knight)
            {
                return CurrentLocation.Equals(((Knight)obj).CurrentLocation);
            }

            return false;
        }
    }
}