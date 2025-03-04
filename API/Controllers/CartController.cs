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
        public async Task<IActionResult> GetCart(string cartId)
        {
            var cart = await _cartService.GetShoppingCartAsync(cartId);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }

        [HttpPost("create-or-update")]
        public async Task<IActionResult> CreateOrUpdateCart([FromBody]ShoppingCart shoppingCart)
        {
            //var newCart = new ShoppingCart()
            //{
            //    Id = Guid.NewGuid()
            //};
            var cart = await _cartService.CreateOrUpdateCartAsync(shoppingCart);
            if (cart != null)
            {
                return Ok(cart);
            }
            return BadRequest();
        }

        [HttpDelete("delete/{id:guid}")]
        public async Task<IActionResult> DeleteCart(string id)
        {
            var result = await _cartService.DeleteCartAsync(id);
            if (result) return NoContent();
            return BadRequest("Cart deletion has a problem");
        }
    }
}
