using Microsoft.AspNetCore.Mvc;
using YourDebtsCore.Base.Models;
using YourDebtsCore.Base.Validations;
using YourDebtsCore.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace YourDebtsCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProductService _productService;

        public ProductController(IHttpContextAccessor httpContextAccessor, IProductService productService)
        {
            _httpContextAccessor = httpContextAccessor;
            _productService = productService;
        }

        // GET: api/<ProductController>
        [HttpGet]
        [Route("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var context = _httpContextAccessor.HttpContext;
            var (currentUser, expired) = ValidationToken.Handler(context);
            if (expired) return Unauthorized("Token Expirado");

            return Ok(await _productService.GetProductsList(currentUser.UserAdminID));
        }

        //// GET api/<ProductController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<ProductController>
        [HttpPost]
        [Route("InsertProduct")]
        public async Task<IActionResult> InsertProduct([FromBody] ProductModel product)
        {
            var context = _httpContextAccessor.HttpContext;
            var (currentUser, expired) = ValidationToken.Handler(context);
            if (expired) return Unauthorized("Token Expirado");

            return Ok(await _productService.InsertProduct(product, currentUser.UserAdminID));
        }

        //// PUT api/<ProductController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<ProductController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
