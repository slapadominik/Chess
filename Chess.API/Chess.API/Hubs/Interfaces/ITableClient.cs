using System;
using System.Threading.Tasks;
using Chess.Logic;

namespace Chess.API.Hubs.Interfaces
{
    public interface ITableClient
    {
        Task JoinGame(int tableNumber, Guid playerId, Color color);
        Task JoinTable(int tableNumber);
        Task NotifyUserJoined(string message);
    }
}