using System.Collections.Generic;
using Chess.Logic.Exceptions;
using Chess.Logic.Interfaces;

namespace Chess.Logic.Figures
{
    public class King : Chessman
    {
        private int[] _validMoves;

        public King(Color color) : base(color)
        {
            _validMoves = new int[] {-9, -8, -7, -1, 1, 7, 8, 9};
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
            ValidateMove(from, to, _validMoves);
            SwapPieces(board, from, to);
            return new MoveResult(from, to, MoveStatus.Normal, GetColor());
        }

        private MoveResult MakeCaptureMove(IBoard board, string from, string to)
        {
            if (board.GetChessman(to).GetColor() == GetColor())
            {
                throw new InvalidMoveException($"Location [{to}] contains friendly chessman!");
            }

            ValidateMove(from, to, _validMoves);
            SwapPieces(board, from, to);
            return new MoveResult(from, to, MoveStatus.Capture, GetColor());
        }


        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj);
        }
    }
}