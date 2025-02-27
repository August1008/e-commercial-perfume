using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Redis
{
    public class ShoppingCart
    {
        public required Guid Id { get; set; }
        public List<CartItem> CartItems { get; set; } = [];
    }
}
