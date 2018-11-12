using System;
using System.Threading.Tasks;
using Chess.API.Entity;
using Chess.API.Hubs;
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
        private readonly IHubContext<TableHub> _contextTableHub;

        public TableController(ILogger<TableController> logger, IHubContext<TableHub> contextTableHub)
        {
            _logger = logger;
            _contextTableHub = contextTableHub;
        }

        [HttpPost("chooseSite")]
        public async Task<IActionResult> ChooseSite([FromBody]DTO.Input.UserColor user)
        {
            try
            {
                //_table.ChooseSite(userDomain, user.Color);
                await _contextTableHub.Clients.All.SendAsync("ChooseSite", user.Color);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}