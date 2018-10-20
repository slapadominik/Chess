using System.Threading.Tasks;
using Chess.API.Hubs.Interfaces;
using Chess.API.Hubs.Models;
using Chess.Logic;
using Microsoft.AspNetCore.SignalR;

namespace Chess.API.Hubs
{
    public class TableHub : Hub<ITableClient>
    {
        public async Task ChooseSite(Color color) => await Clients.All.ChooseSite(color);

        public async Task UserJoined(UserJoined userJoined) => await Clients.All.UserJoin(userJoined);
    }
}