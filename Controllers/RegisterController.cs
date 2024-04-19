using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YourDebtsCore.Base.Models;
using YourDebtsCore.Base.Validations;
using YourDebtsCore.Services;

namespace YourDebtsCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IRegisterService _registerService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RegisterController(IRegisterService registerService, IHttpContextAccessor httpContextAccessor)
        {
            _registerService = registerService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        public IActionResult RegisterUser([FromBody] RegisterModel register)
        {
            return Ok(_registerService.RegisterUser(register));
        }

        [HttpPut]
        [Authorize]
        [Route("UpdateRegister")]
        public async Task<IActionResult> UpdateRegister([FromBody] RegisterModel updateData)
        {
            var context = _httpContextAccessor.HttpContext;
            var (currentUser, expired) = ValidationToken.Handler(context);
            if (expired) return Unauthorized("Token Expirado");

            return Ok(await _registerService.UpdateRegister(updateData, currentUser));
        }
    }
}
