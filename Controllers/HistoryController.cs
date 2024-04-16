using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YourDebtsCore.Base.Validations;
using YourDebtsCore.Services;

namespace YourDebtsCore.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHistoryService _historyService;

        public HistoryController(IHttpContextAccessor httpContextAccessor, IHistoryService historyService)
        {
            _httpContextAccessor = httpContextAccessor;
            _historyService = historyService;
        }

        [HttpGet]
        [Route("GetHistoryOtherDebts")]
        public async Task<IActionResult> GetHistory_otherDebts()
        {
            var context = _httpContextAccessor.HttpContext;
            var (currentUser, expired) = ValidationToken.Handler(context);
            if (expired) return Unauthorized("Token Expirado");


            return Ok(await _historyService.GetHistoryOtherDebt(currentUser.UserAdminID));
        }

        [HttpGet]
        [Route("GetHistoryAbono")]
        public async Task<IActionResult> GetHistory_Abono()
        {
            var context = _httpContextAccessor.HttpContext;
            var (currentUser, expired) = ValidationToken.Handler(context);
            if (expired) return Unauthorized("Token Expirado");


            return Ok(await _historyService.GetHistoryAbono(currentUser.UserAdminID));
        }

        [HttpGet]
        [Route("GetHistoryProducts")]
        public async Task<IActionResult> GetHistory_Products()
        {
            var context = _httpContextAccessor.HttpContext;
            var (currentUser, expired) = ValidationToken.Handler(context);
            if (expired) return Unauthorized("Token Expirado");


            return Ok(await _historyService.GetHistoryProducts(currentUser.UserAdminID));
        }
    }
}
