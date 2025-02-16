using Core.Entities;
using Core.Interfaces;
using Core.Models.RequestModels;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery]GetProductsModel model)
        {
            var products = await _productRepository.GetProductsAsync(model);
            return Ok(products);
        }

        [HttpGet("types")]
        public async Task<IActionResult> GetTypes()
        {
            var types = await _productRepository.GetTypes();
            return Ok(types);
        }

        [HttpGet("brands")]
        public async Task<IActionResult> GetBrands()
        {
            var brands = await _productRepository.GetBrands();
            return Ok(brands);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody]CreateProductModel model)
        {
            var product = new Product
            {
                Name = model.Name,
                Brand = model.Brand,
                Type = model.Type,
                Description = model.Description,
                PictureUrl = model.PictureUrl,
                Price = model.Price,
                Quantity = model.Quantity
            };
            _productRepository.Add(product);
            await _productRepository.SaveChangesAsync();
            return Ok(product);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if(product == null) return NotFound();
            _productRepository.Remove(product);
            await _productRepository.SaveChangesAsync();
            return NoContent();
        }
    }
}
