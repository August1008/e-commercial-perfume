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

        public async Task<ShoppingCart?> CreateCartAsync(ShoppingCart cart)
        {
            if (cart == null) { return  null; }
            var cartData = JsonSerializer.Serialize<ShoppingCart>(cart);
            var expiration = TimeSpan.FromMinutes(10);
            var cartKey = GetCartKey(cart.Id);
            var result = await _redisDatabase.StringSetAsync(cartKey, cartData, expiry: expiration);
            if (result) return cart;
            return null;
        }

        public Task<bool> DeleteCartAsync(string cartId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> FlushCartAsync(string cartId)
        {
            throw new NotImplementedException();
        }

        public async Task<ShoppingCart> GetShoppingCartAsync(Guid cartId)
        {
            var cartkey = GetCartKey(cartId);
            var cartData = await _redisDatabase.StringGetAsync(cartkey);
            var cart = JsonSerializer.Deserialize<ShoppingCart>(cartData);
            return cart;
        }

        private string GetCartKey(Guid cartId)
        {
            return $"Cart:{cartId.ToString()}";
        }
    }
}
