using System;
using System.Collections.Generic;
using System.Linq;
using Chess.API.Entity;
using Chess.API.Entity.Interfaces;
using Chess.API.Helpers;
using Chess.Logic;
using Chess.Logic.Interfaces;

namespace Chess.API.Services.Interfaces
{
    public class TableService : ITableService
    {
        private static readonly List<ITable> _tables;
        private readonly IGameManager _gameManager;

        static TableService()
        {
            _tables = new List<ITable>(){new Table(1)};
        }

        public TableService(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public int AddTable()
        {
            var tableNumber = TableNumberCalculator.CalculateFreeTableNumber(_tables);
            _tables.Add(new Table(tableNumber));
            return tableNumber;
        }

        public IEnumerable<ITable> GetTables()
        {
            return _tables;
        }

        public void JoinGame(int tableNumber, Guid playerId, Color color)
        {
            var table = _tables.Single(x => x.Number == tableNumber);
            if (table.IsFull())
            {
                throw new InvalidOperationException($"Table {table.Number} is full! You cannot join the table.");
            }
            table.JoinTable(playerId, color);
        }

        public Guid CreateGame(int tableNumber, Guid participantPlayer)
        {
            var table = _tables.Single(x => x.Number == tableNumber);
            if (table.PlayerBlackId != participantPlayer && table.PlayerWhiteId != participantPlayer)
            {
                throw new InvalidOperationException($"User {participantPlayer} is not a Player on Table {tableNumber}");
            }
            if (!table.IsFull())
            {
                throw new InvalidOperationException($"Table {table.Number} is not full! You cannot create game.");
            }
            return _gameManager.CreateGame(table.PlayerWhiteId, table.PlayerBlackId);
        }
    }
}