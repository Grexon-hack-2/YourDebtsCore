using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YourDebtsCore.Base.Models;
using YourDebtsCore.Base.Validations;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DebtsController(IDebtService debtService, IHttpContextAccessor httpContextAccessor) 
        { 
            _debtService = debtService;
            _httpContextAccessor = httpContextAccessor;
        }

        //// GET: api/<DebtsController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<DebtsController>/5
        [HttpGet]
        [Route("GetAllOtherDebts")]
        public async Task<IActionResult> GetAllOtherDebts()
        {
            var context = _httpContextAccessor.HttpContext;
            var (currentUser, expired) = ValidationToken.Handler(context);
            if (expired) return Unauthorized("Token Expirado");

            return Ok(await _debtService.GetAllOtherDebts(currentUser.UserAdminID));
        }

        [HttpGet]
        [Route("GetOtherDebtById")]
        public async Task<IActionResult> GetOtherDebtById([FromQuery] Guid debtorID)
        {
            var context = _httpContextAccessor.HttpContext;
            var (currentUser, expired) = ValidationToken.Handler(context);
            if (expired) return Unauthorized("Token Expirado");

            return Ok(await _debtService.GetOtherDebtById(currentUser.UserAdminID, debtorID));
        }

        // POST api/<DebtsController>
        [HttpPost]
        [Route("InsertingDebt")]
        public async Task<IActionResult> InsertingDebt([FromBody] DebtRegisterModel value)
        {
            return Ok(await _debtService.InsertDebt(value));
        }

        [HttpPost]
        [Route("AddAbonoToUser")]
        public async Task<IActionResult> PayTheDebt([FromBody] PayDebtsModel value)
        {
            return Ok(await _debtService.InsertPay(value));
        }

        [HttpPost]
        [Route("InsertingOtherDebt")]
        public async Task<IActionResult> InsertingOtherDebt([FromBody] OtherDebtsRequestModel value)
        {
            var context = _httpContextAccessor.HttpContext;
            var (currentUser, expired) = ValidationToken.Handler(context);
            if (expired) return Unauthorized("Token Expirado");

            return Ok(await _debtService.InsertOtherDebt(value, currentUser.UserAdminID));
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
