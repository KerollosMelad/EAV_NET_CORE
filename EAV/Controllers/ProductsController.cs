using EAV.Data.EFCore;
using EAV.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EAV.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : MyController<Product, ProductRepo>
    {
        private readonly ProductRepo _repository;

        public ProductsController(ProductRepo repository) : base(repository)
        {
            _repository = repository;
        }

        // POST: api/[controller]
        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct()
        {
            await _repository.AddMockProduct();
            return NoContent();
        }

        [HttpPost("AddProducts")]
        public async Task<IActionResult> AddProducts()
        {
            await _repository.AddMockProducts();
            return NoContent();
        }

        [HttpGet("{id}/withProp")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var entity = await _repository.GetProduct(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }
    }
}
