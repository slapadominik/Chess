using System;
using Chess.API.Entity;
using Chess.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Chess.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("create")]
        public IActionResult CreateUser()
        {
            var id = _userService.CreateUser();
            return Ok(id);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(Guid id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound($"User with given id {id} doesn't exist");
            }
            return Ok(user);
        } 
    }
}