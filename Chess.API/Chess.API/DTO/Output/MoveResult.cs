using Chess.Logic;

namespace Chess.API.DTO.Output
{
    public class MoveResult
    {
        public string From { get; set; }
        public string To { get; set; }
        public MoveStatus MoveStatus { get; set; }
        public string CurrentPlayer { get; set; }
    }
}