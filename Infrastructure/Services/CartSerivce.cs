using Core.Entities.Redis;
using Core.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class CartSerivce : ICartService
    {
        private readonly IDatabase _redisDatabase;
        public CartSerivce(IConnectionMultiplexer redisConnection)
        {
            _redisDatabase = redisConnection.GetDatabase();
        }

        public async Task<ShoppingCart?> CreateOrUpdateCartAsync(ShoppingCart cart)
        {
            if (cart != null)
            {
                cart.Id = IsValidCartKey(cart.Id) ? cart.Id : Guid.NewGuid().ToString();
                var cartData = JsonSerializer.Serialize<ShoppingCart>(cart);
                var expiration = TimeSpan.FromDays(10);
                var cartKey = GetCartKey(cart.Id);
                var result = await _redisDatabase.StringSetAsync(cartKey, cartData, expiry: expiration);
                if (result) return cart;
            }
            return null;
        }

        public async Task<bool> DeleteCartAsync(string cartId)
        {
            return await _redisDatabase.KeyDeleteAsync(GetCartKey(cartId));
        }

        public async Task<bool> FlushCartAsync(string cartId)
        {
            var cartkey = GetCartKey(cartId);
            var cartData = await _redisDatabase.StringGetAsync(cartkey);
            if (!cartData.IsNullOrEmpty)
            {
                var cart = JsonSerializer.Deserialize<ShoppingCart>(cartData);
                cart.CartItems.Clear();

                var updatedCartData = JsonSerializer.Serialize<ShoppingCart>(cart);
                var cartKey = GetCartKey(cartId);
                return await _redisDatabase.StringSetAsync(cartKey, updatedCartData);
            }
            return false;

        }

        public async Task<ShoppingCart?> GetShoppingCartAsync(string cartId)
        {
            var cartkey = GetCartKey(cartId);
            var cartData = await _redisDatabase.StringGetAsync(cartkey);
            if (cartData.IsNullOrEmpty) return null;
            var cart = JsonSerializer.Deserialize<ShoppingCart>(cartData);
            return cart;
        }



        #region private 

        private string GetCartKey(string cartId)
        {
            return $"cart:{cartId}";
        }

        private bool IsValidCartKey(string cartKey)
        {
            return !string.IsNullOrEmpty(cartKey) && Guid.TryParse(cartKey, out Guid id);
        }

        #endregion
    }
}
