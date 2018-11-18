using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Logic.Helpers;
using Chess.Logic.Interfaces;

namespace Chess.Logic
{
    public class GameManager : IGameManager
    {
        private readonly List<IGame> _games;

        public GameManager()
        {
            _games = new List<IGame>();
        }

        public Guid CreateGame(Guid playerWhiteId, Guid playerBlackId)
        {
            var game = new Game(playerWhiteId, playerBlackId, new Board(new MoveValidator()));
            _games.Add(game);
            return game.GetId();
        }

        public IGame GetGame(Guid gameId)
        {
            return _games.SingleOrDefault(x => x.GetId()==gameId);
        }
    }
}