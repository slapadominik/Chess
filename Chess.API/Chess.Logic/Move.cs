namespace Chess.Logic
{
    public class Move
    {
        public Chessman Figure { get; set; }
        public string From { get; set; }
        public string To { get; set; }

        public Move(Chessman figure, string @from, string to)
        {
            Figure = figure;
            From = @from;
            To = to;
        }
    }
}