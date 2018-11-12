using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chess.API.DTO.Input;
using Chess.API.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Chess.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private static List<string> Source { get; set; } = new List<string>();
        private IHubContext<ValuesHub> _valuesHubContext;

        public ValuesController(IHubContext<ValuesHub> valuesHubContext)
        {
            _valuesHubContext = valuesHubContext;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return Source;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return Source[id];
        }

        // POST api/values
        [HttpPost("add")]
        public async Task<IActionResult> Post([FromBody]ValueDto valuedto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.ValidationState);
            }
            Source.Add(valuedto.Value);
            await _valuesHubContext.Clients.All.SendAsync("Add", valuedto.Value);
            return Ok();
        }
            
        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            Source[id] = value;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Source.RemoveAt(id);
        }
    }
}
