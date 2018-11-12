using Chess.Logic.Interfaces;

namespace Chess.Logic.Figures
{
    public class Bishop : Chessman
    {
        public Bishop(Color color) : base(color)
        {
        }

        public override IBoard MakeMove(IBoard board, string @from, string to)
        {
            throw new System.NotImplementedException();
        }
    }
}