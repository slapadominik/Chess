using System;
using System.Collections.Generic;
using System.Linq;
using Chess.API.Entity;
using Chess.API.Entity.Interfaces;
using Chess.Logic;
using Chess.Logic.Interfaces;

namespace Chess.API.Services.Interfaces
{
    public class TableService : ITableService
    {
        private readonly List<ITable> _tables;
        private readonly IGameManager _gameManager;

        public TableService(IGameManager gameManager)
        {
            _tables = new List<ITable>();
            _gameManager = gameManager;
        }

        public int AddTable()
        {
            var tableNumber = CalculateFreeTableNumber();
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

        private int CalculateFreeTableNumber()
        {
            var tableNumber = -1;
            for (int i = 1; i < _tables.Count; i++)
            {
                if (_tables[i].Number != i)
                {
                    tableNumber = i;
                }
            }

            if (tableNumber == -1)
            {
                tableNumber = _tables.Count + 1;
            }

            return tableNumber;
        }
    }
}