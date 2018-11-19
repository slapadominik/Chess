using System;
using Chess.API.Services.Interfaces;
using Chess.Logic;
using Chess.Logic.Interfaces;

namespace Chess.API.Services
{
    public class GameService : IGameService
    {
        private readonly IGameManager _gameManager;

        public GameService(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public IGame GetGame(Guid gameId)
        {
            return _gameManager.GetGame(gameId);
        }
    }
}