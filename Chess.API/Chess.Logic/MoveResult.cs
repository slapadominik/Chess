using Chess.Logic.Interfaces;

namespace Chess.Logic
{
    public struct MoveResult
    {
        public MoveResult(IBoard board, MoveStatus moveStatus)
        {
            MoveStatus = moveStatus;
            Board = board;
        }

        public MoveStatus MoveStatus { get; set; }
        public IBoard Board { get; set; }
    }
}