using System;
using System.Threading.Tasks;
using Chess.API.DTO.Input;
using Chess.API.Entity;
using Chess.API.Exceptions;
using Chess.API.Hubs;
using Chess.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Chess.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : Controller
    {
        private readonly ILogger<TableController> _logger;
        private readonly ITableService _tableService;

        public TableController(ILogger<TableController> logger, ITableService tableService)
        {
            _logger = logger;
            _tableService = tableService;
        }

        [HttpPost("getTableInfo")]
        public async Task<IActionResult> GetTableInfo([FromBody] TableInfoRequest tableInfoRequest)
        {
            try
            {
                var tableState = _tableService.GetTableState(tableInfoRequest.TableNumber);
                _logger.LogInformation($"Successfully retrieved table state for table [{tableInfoRequest.TableNumber}]");
                return Ok(tableState);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return BadRequest(ex.Message);
            }
        }

    }
}