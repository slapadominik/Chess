using System;
using Chess.API.Exceptions;
using Chess.API.Services.Interfaces;
using Chess.Logic;
using Chess.Logic.Interfaces;
using Microsoft.Extensions.Logging;

namespace Chess.API.Services
{
    public class GameService : IGameService
    {
        private readonly IGameManager _gameManager;
        private readonly ITableService _tableService;
        private readonly ILogger _logger;

        public GameService(IGameManager gameManager, ITableService tableService, ILogger<GameService> logger)
        {
            _gameManager = gameManager;
            _tableService = tableService;
            _logger = logger;
        }

        public IGame GetGame(Guid gameId)
        {
            return _gameManager.GetGame(gameId);
        }

        public void DeleteGames()
        {
            _gameManager.DeleteGames();
        }

        public void StartGame(Guid gameId, Guid userId)
        {
            var game = _gameManager.GetGame(gameId);
            game.StartGame(userId);
            _logger.LogInformation($"Game with id [{game.GetId()}] has started!");
        }

        public Guid CreateGame(int tableNumber, Guid userId)
        {
            var table = _tableService.Get(tableNumber);

            if (table == null)
            {
                throw new TableNotExistException($"Table with number {tableNumber} doesn't exist!");
            }

            if (table.IsPlayer(userId))
            {
                if (table.IsFull())
                {
                    var gameId = _gameManager.CreateGame(table.PlayerWhiteId, table.PlayerBlackId);
                    var game = _gameManager.GetGame(gameId);
                    table.Game = game;
                    return gameId;
                }
                throw new TableNotFullException($"User {userId} cannot create game on table {tableNumber} - table is not full");
            }
            throw new InvalidOperationException($"User {userId} is not a player on table {tableNumber} - he cannot create game");
        }
    }
}