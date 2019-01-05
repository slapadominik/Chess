using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Chess.API.Exceptions;
using Chess.API.Helpers;
using Chess.API.Services.Interfaces;
using Chess.Logic;
using Chess.Logic.Consts;
using Chess.Logic.Exceptions;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Chess.API.Hubs
{
    public class GameHub : Hub
    {
        private readonly ITableService _tableService;
        private readonly IUserService _userService;
        private readonly IGameService _gameService;
        private readonly ILogger _logger;
        private readonly IExceptionHandler _exceptionHandler;

        public GameHub(ITableService tableService, IUserService userService, IGameService gameService, ILogger<GameHub> logger, IExceptionHandler exceptionHandler)
        {
            _tableService = tableService;
            _userService = userService;
            _gameService = gameService;
            _logger = logger;
            _exceptionHandler = exceptionHandler;
        }

        public override async Task OnConnectedAsync()
        {
            var id = _userService.CreateUser();
            var user = _userService.GetUserById(id);
            await Clients.Caller.SendAsync("UserCreated_Caller", user.Id, user.Username);
            await Clients.All.SendAsync("UserCreated_All", user.Username);
            
        }

        public async Task JoinGame(int tableNumber, Guid playerId, Color color)
        {
            try
            {
                var user = _userService.GetUserById(playerId);
                _tableService.JoinTable(tableNumber, playerId, color);

                _logger.LogInformation($"User with id [{playerId}] successfully joined game on table [{tableNumber}] with color {color}");
                await Clients.All.SendAsync("JoinGame_Result", tableNumber, user.Username, color);
            }
            catch (TableNotExistException ex)
            {
                await Clients.Caller.SendAsync("JoinGame_Error", ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                await Clients.Caller.SendAsync("JoinGame_Error", ex.Message);
            }
            catch (UserNotFoundException ex)
            {
                _logger.LogInformation($"User with id [{playerId}] doesn't exist!");
                await Clients.Caller.SendAsync("JoinGame_Error", ex.Message);
            }
        }

        public async Task ResetTable(int tableNumber)
        {
            var table = _tableService.Get(tableNumber);
            if (table == null)
            {
                _logger.LogWarning($"{nameof(ResetTable)} - Table with number {tableNumber} doesn't exist.");
                return;
            }

            table.DismissPlayers();
            table.Game = null;
            _logger.LogWarning($"Dismissed players on table {tableNumber}!");
            _gameService.DeleteGames();
            _logger.LogWarning($"Deleted all games!");

            await Clients.All.SendAsync("ResetTable_result");
        }

        public async Task CreateGame(int tableNumber, Guid userId)
        {
            try
            {
                var gameId = _gameService.CreateGame(tableNumber, userId);
                _logger.LogInformation(
                    $"Game for table [{tableNumber}] has been successfully created with id [{gameId}]");
                await Clients.All.SendAsync($"CreateGame_Result", gameId);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
            }
        }

        public async Task StartGame(Guid gameId, Guid userId)
        {
            try
            {
                _gameService.StartGame(gameId, userId);
                await Clients.All.SendAsync("StartGame_Result", gameId);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
            }    
        }

        public async Task MakeMove(Guid gameId, Guid playerId, string from, string to)
        {
            try
            {
                var game = _gameService.GetGame(gameId);
                var moveResult = game.MakeMove(playerId, from, to);
                await Clients.All.SendAsync("MakeMove_Result", moveResult);
            }
            catch (Exception ex)
            {
                _exceptionHandler.Handle(ex);
                await Clients.Caller.SendAsync("MakeMove_Error", ex.Message);
            }
        }

        public async Task WriteMessage(string msg, string username)
        {
            await Clients.All.SendAsync("WriteMesssage_Result", username, msg);
        }

        public async Task UserDisconnected(string msg)
        {
            await Clients.All.SendAsync("UserDisconnected", msg);
        }

        private string GetTableGroupName(int tableNumber)
        {
            return "Table" + tableNumber;
        }
    }
}