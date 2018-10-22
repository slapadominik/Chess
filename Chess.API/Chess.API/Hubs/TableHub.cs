using System;
using System.Threading.Tasks;
using Chess.API.Entity.Interfaces;
using Chess.API.Hubs.Interfaces;
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

        public async Task JoinTable(int tableNumber, Guid playerId)
        {
            var user = _userService.GetUserById(playerId);
            var groupName = GetTableGroupName(tableNumber);
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            //await Clients.Groups(groupName).SendAsync("NotifyUserJoined", user.Username);
            await Clients.All.SendAsync("NotifyUserJoined", user.Username);
        }

        public async Task JoinGame(int tableNumber, Guid playerId, Color color)
        {
            _tableService.JoinGame(tableNumber, playerId, color);
             await Clients.All.SendAsync("JoinGame", tableNumber, playerId, color);
        }

        private string GetTableGroupName(int tableNumber)
        {
            return "Table" + tableNumber;
        }

    }
}