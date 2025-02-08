using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        public ProductsController() { }
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            return Ok();
        }
    }
}
