using Core.Entities;
using Core.Helper;
using Core.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        public Task<PaginatedResult<Product>> GetProductsAsync(GetProductsModel model);
        public Task<IReadOnlyList<string>> GetBrands();
        public Task<IReadOnlyList<string>> GetTypes();

    }
}
