using System;
using System.Threading.Tasks;
using Chess.API.Exceptions;
using Chess.API.Services.Interfaces;
using Chess.Logic;
using Chess.Logic.Exceptions;
using Microsoft.AspNetCore.SignalR;

namespace Chess.API.Hubs
{
    public class GameHub : Hub
    {
        private readonly ITableService _tableService;
        private readonly IUserService _userService;
        private readonly IGameService _gameService;

        public GameHub(ITableService tableService, IUserService userService, IGameService gameService)
        {
            _tableService = tableService;
            _userService = userService;
            _gameService = gameService;
        }

        public override async Task OnConnectedAsync()
        {
            var id = _userService.CreateUser();
            var user = _userService.GetUserById(id);
            await Clients.Caller.SendAsync("CreateUser_Caller", user.Id, user.Username);
            await Clients.All.SendAsync("UserCreated_All", user.Id, user.Username);
        }

        public async Task JoinGame(int tableNumber, Guid playerId, Color color)
        {
            try
            {
                _tableService.JoinGame(tableNumber, playerId, color);
                var user = _userService.GetUserById(playerId);
                await Clients.All.SendAsync("JoinGame", tableNumber, user.Username, color);
            }
            catch (InvalidOperationException ex)
            {
                await Clients.Caller.SendAsync("JoinGame_Error", ex.Message);
            }
            catch (UserNotFoundException ex)
            {
                await Clients.Caller.SendAsync("JoinGame_Error", ex.Message);
            }
        }

        public async Task CreateGame(int tableNumber, Guid participantPlayer)
        {
            try
            {
                var gameId = _tableService.CreateGame(tableNumber, participantPlayer);
                await Clients.All.SendAsync("GameCreated", gameId);
            }
            catch (InvalidOperationException ex)
            {
                await Clients.Caller.SendAsync("CreateGame_Error", ex.Message);
            }
        }

        public async Task StartGame(Guid gameId, Guid participantPlayer)
        {
            try
            {
                var game = _gameService.GetGame(gameId);
                game.StartGame();
                await Clients.All.SendAsync("StartGame_Result");
            }
            catch (GameNotExistException ex)
            {
                await Clients.Caller.SendAsync("GameStarted_Error", ex.Message);
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
            catch (GameNotExistException ex)
            {
                await Clients.Caller.SendAsync("MakeMove_Error", ex.Message);
            }
        }

        public async Task UserDisconnected(Guid userId)
        {
            await Clients.All.SendAsync("UserDisconnected", userId);
        }

        private string GetTableGroupName(int tableNumber)
        {
            return "Table" + tableNumber;
        }
    }
}