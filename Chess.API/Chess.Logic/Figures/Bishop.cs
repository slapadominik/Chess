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

        public override bool Equals(object obj)
        {
            if (Equals(obj))
            {
                return true;
            }

            if (obj is Bishop)
            {
                return GetColor() == (obj as Bishop).GetColor();
            }

            return false;
        }
    }
}