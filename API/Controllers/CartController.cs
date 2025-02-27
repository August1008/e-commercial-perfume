using Core.Entities.Redis;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("get-cart/{cartId:guid}")]
        public async Task<IActionResult> GetCart(Guid cartId)
        {
            var cart = await _cartService.GetShoppingCartAsync(cartId);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }

        [HttpPost("create-cart")]
        public async Task<IActionResult> CreateCart()
        {
            var newCart = new ShoppingCart()
            {
                Id = Guid.NewGuid()
            };
            var cart = await _cartService.CreateCartAsync(newCart);
            if (cart != null)
            {
                return Ok(cart);
            }
            return BadRequest();
        }
    }
}
