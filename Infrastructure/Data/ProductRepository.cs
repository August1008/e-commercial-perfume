using Core.Entities;
using Core.Interfaces;
using Infrastructure.Extensions;
using Core.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Core.Models.RequestModels;

namespace Infrastructure.Data
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(StoreContext context) : base(context)
        {
        }

        public async Task<PaginatedResult<Product>> GetProductsAsync(GetProductsModel model)
        {
            var brands = model.brand?.Split(",");
            var types = model.type?.Split(",");

            var products = _context.Products.Where(item => 
            (string.IsNullOrEmpty(model.brand) || (brands != null && brands.Any(b => b == item.Brand))) && 
            (string.IsNullOrEmpty(model.type) || (types != null && types.Any(t => t == item.Type))) &&
            (string.IsNullOrEmpty(model.Search) || item.Name.ToLower().Contains(model.Search)));

            products = model.sortType switch
            {
                "priceAsc" => products.OrderBy(p => p.Price),
                "priceDes" => products.OrderByDescending(p => p.Price),
                "nameAsc" => products.OrderBy(p => p.Name),
                _ => products
            };
            return products.Paginate(model.page, model.pageSize);
        }

        public async Task<IReadOnlyList<string>> GetBrands()
        {
            var brands = await _context.Products.Select(x => x.Brand).Distinct().ToListAsync();
            return brands;
        }

        public async Task<IReadOnlyList<string>> GetTypes()
        {
            var types = await _context.Products.Select(x => x.Type).Distinct().ToListAsync();
            return types;
        }
    }
}
