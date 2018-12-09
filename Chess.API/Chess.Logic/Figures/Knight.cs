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
            _validMoves = new int[] {-17, 17, -15, 15, 10, -10, 6};
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