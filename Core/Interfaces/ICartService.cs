using Core.Entities.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ICartService
    {
        public Task<ShoppingCart?> CreateCartAsync(ShoppingCart cart);
        public Task<ShoppingCart> GetShoppingCartAsync(Guid cartId);
        public Task<bool> DeleteCartAsync(string cartId);
        public Task<bool> FlushCartAsync(string cartId);
    }
}
