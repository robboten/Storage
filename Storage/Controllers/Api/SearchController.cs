using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Storage.Models;

namespace Storage.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IProductRepository productRepository;

        public SearchController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var allProducts = productRepository.AllProducts;
            return Ok(allProducts);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            if(!productRepository.AllProducts.Any(p=>p.Id==id))
                return NotFound();

            return Ok(productRepository.AllProducts.Where(p => p.Id == id));
        }
    }
}
