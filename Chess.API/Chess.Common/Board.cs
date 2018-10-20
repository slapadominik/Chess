namespace Chess.Common
{
    public class Board : IBoard
    {
        private Field[,] _fields { get; set; }

        public Board()
        {
            _fields = new Field[8, 8]
            {
                {new Field("A8"), new Field("B8"), new Field("C8"), new Field("D8"), new Field("E8"), new Field("F8"), new Field("G8"), new Field("H8")},
                {new Field("A7"), new Field("B7"), new Field("C7"), new Field("D7"), new Field("E7"), new Field("F7"), new Field("G7"), new Field("H7")},
                {new Field("A6"), new Field("B6"), new Field("C6"), new Field("D6"), new Field("E6"), new Field("F6"), new Field("G6"), new Field("H6")},
                {new Field("A5"), new Field("B5"), new Field("C5"), new Field("D5"), new Field("E5"), new Field("F5"), new Field("G5"), new Field("H5")},
                {new Field("A4"), new Field("B4"), new Field("C4"), new Field("D4"), new Field("E4"), new Field("F4"), new Field("G4"), new Field("H4")},
                {new Field("A3"), new Field("B3"), new Field("C3"), new Field("D3"), new Field("E3"), new Field("F3"), new Field("G3"), new Field("H3")},
                {new Field("A2"), new Field("B2"), new Field("C2"), new Field("D2"), new Field("E2"), new Field("F2"), new Field("G2"), new Field("H2")},
                {new Field("A1"), new Field("B1"), new Field("C1"), new Field("D1"), new Field("E1"), new Field("F1"), new Field("G1"), new Field("H1")}
            };
        }

        public bool MakeMove(Player player, Field @from, Field to)
        {
            throw new System.NotImplementedException();
        }

        private bool ValidateMove(Player player, Field @from, Field to)
        {
            return true;
        }
    }
}