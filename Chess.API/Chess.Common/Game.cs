using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Chess.Common
{
    public class Game : IGame
    {
        public int MoveCounter { get; private set; }
        public Stopwatch Timer { get; private set; }
        public Player CurrentPlayer { get; }
        public IBoard Board { get; private set; }

        private Player _playerWhite { get; set; }
        private Player _playerBlack { get; set; }
        

        public Game(Player playerWhite, Player playerBlack)
        {
            _playerWhite = playerWhite;
            _playerBlack = playerBlack;
            Board = new Board();
            Timer = new Stopwatch();
            MoveCounter = 0;
        }

        public bool MakeMove(Player player, Field @from, Field to)
        {
            if (!CanMakeMove(player))
                return false;

            return true;
        }

        private bool CanMakeMove(Player player)
        {
            return player.Equals(CurrentPlayer);
        }

        public void StartGame()
        {
            Timer.Start();
        }
    }
}
