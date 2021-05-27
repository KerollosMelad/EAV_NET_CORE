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
        [HttpPost("Add")]
        public async Task<IActionResult> Post()
        {
            await _repository.AddMockProducts();
            return NoContent();
        }
    }
}
