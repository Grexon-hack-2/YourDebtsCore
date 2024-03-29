using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YourDebtsCore.Base.Models;
using YourDebtsCore.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace YourDebtsCore.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DebtsController : ControllerBase
    {
        private readonly IDebtService _debtService;
        public DebtsController(IDebtService debtService) 
        { 
            _debtService = debtService;
        }

        //// GET: api/<DebtsController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<DebtsController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<DebtsController>
        [HttpPost]
        [Route("InsertingDebt")]
        public async Task<IActionResult> InsertingDebt([FromBody] DebtRegisterModel value)
        {
            return Ok(await _debtService.InsertDebt(value));
        }

        //// PUT api/<DebtsController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<DebtsController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
