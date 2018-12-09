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

        public Bishop(Color color, string currentLocation) : base(color, currentLocation)
        {
            _validMovesTopLeft = Enumerable.Range(1, 7).Select(a => a * (-9));
            _validMovesTopRight = Enumerable.Range(1, 7).Select(y => y * (-7));
            _validMovesDownLeft = Enumerable.Range(1, 7).Select(z => z * 7);
            _validMovesDownRight = Enumerable.Range(1, 7).Select(a => a * 9);
        }

        public override MoveResult Move(IBoard board, string to)
        {
            if (board.GetChessman(to) == null)
            {
                return MakeNonCaptureMove(board, CurrentLocation, to);
            }

            return MakeCaptureMove(board, CurrentLocation, to);
        }

        private MoveResult MakeNonCaptureMove(IBoard board, string from, string to)
        {
            ValidateMove(to, _validMovesDownRight);
            MoveToDestination(board, to);
            return new MoveResult(from, to, MoveStatus.Normal, GetColor());
        }

        private MoveResult MakeCaptureMove(IBoard board, string from, string to)
        {
            if (board.GetChessman(to).GetColor() == GetColor())
            {
                throw new InvalidMoveException($"Location [{to}] contains friendly chessman!");
            }

            ValidateMove(to, _validMovesDownRight);
            MoveToDestination(board, to);
            return new MoveResult(from, to, MoveStatus.Capture, GetColor());
        }

        protected override void ValidateMove(string to, IEnumerable<int> validMoves)
        {
            if (LocationToNumberMapper.ContainsKey(to))
            {
                var valueFrom = LocationToNumberMapper[CurrentLocation];
                var valueTo = LocationToNumberMapper[to];
                foreach (var validMove in validMoves)
                {
                    if (valueTo - valueFrom == validMove)
                    {
                        return;
                    }
                }
            }

            throw new InvalidMoveException($"{GetType()} cannot make move: {CurrentLocation}:{to}");
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj);
        }

    }
}