using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chess.API.Entity.Interfaces;
using Chess.Logic;

namespace Chess.API.Services.Interfaces
{
    public interface ITableService
    {
        int AddTable();
        IEnumerable<ITable> GetTables();
        void JoinGame(int tableNumber, Guid playerId, Color color);
        Guid CreateGame(int tableNumber);
    }
}