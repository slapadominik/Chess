using System;

namespace Chess.API.DTO.Output
{
    public class TableState
    {
        public string PlayerBlackUsername { get; set; }
        public string PlayerWhiteUsername { get; set; }
        public Guid? GameId { get; set; }
        public bool GameStarted { get; set; }
    }
}