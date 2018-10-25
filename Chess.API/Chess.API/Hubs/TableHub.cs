using System;
using System.Threading.Tasks;
using Chess.API.Services.Interfaces;
using Chess.Logic;
using Microsoft.AspNetCore.SignalR;

namespace Chess.API.Hubs
{
    public class TableHub : Hub
    {
        private readonly ITableService _tableService;
        private readonly IUserService _userService;

        public TableHub(ITableService tableService, IUserService userService)
        {
            _tableService = tableService;
            _userService = userService;
        }

        public override async Task OnConnectedAsync()
        {
            var id = _userService.CreateUser();
            var user = _userService.GetUserById(id);
            await Clients.Caller.SendAsync("CreateUser_Caller", user.Id, user.Username);
            await Clients.All.SendAsync("UserCreated_All", user.Id, user.Username);
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        public async Task JoinGame(int tableNumber, Guid playerId, Color color)
        {
            _tableService.JoinGame(tableNumber, playerId, color);
             await Clients.All.SendAsync("JoinGame", tableNumber, playerId, color);
        }

        public async Task CreateGame(int tableNumber, Guid participantPlayer)
        {
            var gameId = _tableService.CreateGame(tableNumber, participantPlayer);
            await Clients.All.SendAsync("GameStarted", gameId);
        }

        private string GetTableGroupName(int tableNumber)
        {
            return "Table" + tableNumber;
        }

    }
}