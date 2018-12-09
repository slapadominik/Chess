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

        public Bishop(Color color) : base(color)
        {
            _validMovesTopLeft = Enumerable.Range(1, 7).Select(a => a * (-9));
            _validMovesTopRight = Enumerable.Range(1, 7).Select(y => y * (-7));
            _validMovesDownLeft = Enumerable.Range(1, 7).Select(z => z * 7);
            _validMovesDownRight = Enumerable.Range(1, 7).Select(a => a * 9);
        }

        public override MoveResult Move(IBoard board, string @from, string to)
        {
            if (board.GetChessman(to) == null)
            {
                return MakeNonCaptureMove(board, from, to);
            }

            return MakeCaptureMove(board, from, to);
        }

        private MoveResult MakeNonCaptureMove(IBoard board, string from, string to)
        {
            ValidateMove(from, to, _validMovesDownRight);
            Move(board, from, to);
            return new MoveResult(from, to, MoveStatus.Normal, GetColor());
        }

        private MoveResult MakeCaptureMove(IBoard board, string from, string to)
        {
            if (board.GetChessman(to).GetColor() == GetColor())
            {
                throw new InvalidMoveException($"Location [{to}] contains friendly chessman!");
            }

            ValidateMove( from, to, _validMovesDownRight);
            Move(board, from, to);
            return new MoveResult(from, to, MoveStatus.Capture, GetColor());
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj);
        }

    }
}