using System.Collections.Generic;
using System.Linq;
using Chess.Logic.Exceptions;
using Chess.Logic.Interfaces;

namespace Chess.Logic.Figures
{
    public class Knight : Chessman
    {
        private int[] _validMoves;

        public Knight(Color color) : base(color)
        {
            _validMoves = new int[] {-17, 17, -15, 15, 10, -10, 6};
        }

        public override MoveStatus MakeMove(IBoard board, string @from, string to)
        {
            if (board.GetChessman(to) == null)
            {
                return MakeNonCaptureMove(board, from, to);
            }

            return MakeCaptureMove(board, from, to);
        }

        private MoveStatus MakeNonCaptureMove(IBoard board, string from, string to)
        {

            ValidateMove(board, from, to, _validMoves);
            Move(board, from, to);
            return MoveStatus.Normal;
        }

        private MoveStatus MakeCaptureMove(IBoard board, string from, string to)
        {
            if (board.GetChessman(to).GetColor() == GetColor())
            {
                throw new InvalidMoveException($"Location [{to}] contains friendly chessman!");
            }

            ValidateMove(board, from, to, _validMoves);

            Move(board, from, to);
            return MoveStatus.Capture;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj);
        }
    }
}