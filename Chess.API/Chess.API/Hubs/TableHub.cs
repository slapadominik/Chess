using System;
using System.Threading.Tasks;
using Chess.API.Exceptions;
using Chess.API.Services.Interfaces;
using Chess.Logic;
using Chess.Logic.Exceptions;
using Microsoft.AspNetCore.SignalR;

namespace Chess.API.Hubs
{
    public class TableHub : Hub
    {
        private readonly ITableService _tableService;

        public TableHub(ITableService tableService)
        {
            _tableService = tableService;
        }

    }
}