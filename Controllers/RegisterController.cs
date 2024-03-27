using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YourDebtsCore.Base.Models;
using YourDebtsCore.Services;

namespace YourDebtsCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IRegisterService registerService;

        public RegisterController(IRegisterService registerService)
        {
            this.registerService = registerService;
        }

        [HttpPost]
        public IActionResult RegisterUser([FromBody] RegisterModel register)
        {
            return Ok(this.registerService.RegisterUser(register));
        }
    }
}
