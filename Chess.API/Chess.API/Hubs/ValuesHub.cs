using System.Threading.Tasks;
using Chess.API.Hubs.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Chess.API.Hubs
{
    public class ValuesHub : Hub<IValuesClient>
    {
        public async Task Add(string value) => await Clients.All.PostValue(value);
        public async Task Delete(string value) => await Clients.All.DeleteValue(value);
    }
}