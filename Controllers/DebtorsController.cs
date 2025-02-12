﻿using Microsoft.AspNetCore.Authorization;
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
    public class DebtorsController : ControllerBase
    {

        private readonly IDebtorsService _debtorsService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DebtorsController(IDebtorsService debtorsService, IHttpContextAccessor httpContextAccessor)
        {
            _debtorsService = debtorsService;
            _httpContextAccessor = httpContextAccessor; 
        }

        // GET: api/<DebtorsController>
        [HttpGet]
        [Route("GetAllDebts")]
        public IActionResult GetAllDebts()
        {
            var context = _httpContextAccessor.HttpContext;
            var (currentUser, expired) = ValidationToken.Handler(context);
            if (expired) return Unauthorized("Token Expirado");

            return Ok(_debtorsService.GetDebtors(currentUser.UserAdminID));
        }

        // GET api/<DebtorsController>/5
        [HttpGet]
        [Route("GetDataFullDebt")]
        public async Task<IActionResult> GetDataFullDebt([FromQuery] Guid idClient)
        {
            if(string.IsNullOrEmpty(idClient.ToString()))
            {
                return BadRequest("El parametro es requerido, por favor ingrese uno valido");
            }

            return Ok(await _debtorsService.DataFullService(idClient));
        }

        // POST api/<DebtorsController>
        [HttpPost]
        [Route("InsertClient")]
        public IActionResult InsertClient([FromBody] DebtorModel value)
        {
            var context = _httpContextAccessor.HttpContext;
            var (currentUser, expired) = ValidationToken.Handler(context);
            if (expired) return Unauthorized("Token Expirado");

            return Ok(_debtorsService.AddNewClient(value, currentUser.UserAdminID));
        }

        // PUT api/<DebtorsController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<DebtorsController>/5
        [HttpDelete("{idClient}")]
        [Route("DeleteClient")]
        public async Task<IActionResult> Delete([FromQuery] Guid idClient)
        {
            return Ok(await _debtorsService.DeleteDebtService(idClient));
        }
    }
}
