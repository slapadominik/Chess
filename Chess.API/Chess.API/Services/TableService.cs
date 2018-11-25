using System;
using System.Collections.Generic;
using System.Linq;
using Chess.API.DTO.Output;
using Chess.API.Entity;
using Chess.API.Entity.Interfaces;
using Chess.API.Exceptions;
using Chess.API.Helpers;
using Chess.API.Services.Interfaces;
using Chess.Logic;
using Chess.Logic.Interfaces;

namespace Chess.API.Services
{
    public class TableService : ITableService
    {
        private static readonly List<ITable> _tables;
        private readonly IUserService _userService;

        static TableService()
        {
            _tables = new List<ITable>(){new Table(1)};
        }

        public TableService(IUserService userService)
        {
            _userService = userService;
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

        public ITable Get(int tableNumber)
        {
            return _tables.SingleOrDefault(x => x.Number==tableNumber);
        }

        public void JoinTable(int tableNumber, Guid playerId, Color color)
        {
            var table = _tables.SingleOrDefault(x => x.Number == tableNumber);
            if (table == null)
            {
                throw new TableNotExistException($"Table [{tableNumber}] doesn't exist!");
            }
            if (table.IsFull())
            {
                throw new InvalidOperationException($"Table {table.Number} is full! You cannot join the table.");
            }
            table.JoinTable(playerId, color);
        }

        public TableState GetTableState(int tableNumber)
        {
            var table = Get(tableNumber);
            if (table == null)
            {
                throw new TableNotExistException($"{nameof(GetTableState)} Table [{tableNumber}] does not exist!");
            }

            Guid? gameId = null;
            bool gameStarted = false;

            if (table.Game != null)
            {
                gameId = table.Game.GetId();
                gameStarted = table.Game.IsGameStarted();
            }

            User playerWhite = null;
            User playerBlack = null;
            try
            {
                playerBlack = _userService.GetUserById(table.PlayerBlackId);
            }
            catch (UserNotFoundException ex){}

            try
            {
                playerWhite = _userService.GetUserById(table.PlayerWhiteId);
            }
            catch(UserNotFoundException ex) { }

            return new TableState
            {
                GameId = gameId,
                PlayerWhiteUsername = playerWhite?.Username,
                PlayerBlackUsername = playerBlack?.Username,
                GameStarted = gameStarted
            };
        }
    }
}