using Chess.Logic.Interfaces;

namespace Chess.Logic.Figures
{
    public class Queen : Chessman
    {
        public Queen(Color color, string currentLocation) : base(color, currentLocation)
        {
        }

        public override MoveResult Move(IBoard board, string to)
        {
            throw new System.NotImplementedException();
        }

        public override bool CanAttackField(IBoard board, string to)
        {
            return false;
        }
    }
}