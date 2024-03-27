using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YourDebtsCore.Base.Autorization;
using YourDebtsCore.Base.Models;

namespace YourDebtsCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthorizationService _authorizationService;

        public LoginController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [HttpPost]
        [Route("Auth")]
        public IActionResult AuthUser([FromBody] AuthorizationRequest request)
        {
            var resultado_auth = _authorizationService.GetAuthorizationToken(request);

            if(resultado_auth.Authorized == false)
                return Unauthorized(resultado_auth);

            return Ok(resultado_auth);
        }
    }
}
