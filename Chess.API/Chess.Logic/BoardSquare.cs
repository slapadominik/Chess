namespace Chess.Logic
{
    public struct BoardSquare
    {
        public string Location { get; set; }
        public string Figure { get; set; }

        public BoardSquare(string location, string figure)
        {
            Location = location;
            Figure = figure;
        }
    }
}