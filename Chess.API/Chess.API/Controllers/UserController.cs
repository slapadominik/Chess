using System.Threading.Tasks;
using Chess.API.Entity;
using Chess.API.Hubs;
using Chess.API.Hubs.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Chess.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        [HttpGet]
        public IActionResult CreateUser()
        {
            User user = new User();
            return Ok(user.Id);
        }
    }
}