using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace YourDebtsCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DebtsController : ControllerBase
    {
        // GET: api/<DebtsController>
        [HttpGet]
        public IEnumerable<string> GetAllDebts()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<DebtsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<DebtsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<DebtsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DebtsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
