using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chess.API.DTO.Output;
using Chess.API.Entity.Interfaces;
using Chess.Logic;
using Chess.Logic.Interfaces;

namespace Chess.API.Services.Interfaces
{
    public interface ITableService
    {
        int AddTable();
        IEnumerable<ITable> GetTables();
        ITable Get(int tableNumber);
        void JoinTable(int tableNumber, Guid playerId, Color color);
        TableState GetTableState(int tableNumber);
    }
}