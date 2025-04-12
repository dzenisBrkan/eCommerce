using AbySalto.Mid.WebApi.Models;
using AbySalto.Mid.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AbySalto.Mid.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<JsonResponse>> Get()
        {
            var products = await _productService.GetAllProducts();
            //if (products == null || products.Count == 0)
            //{
            //    return NotFound("No products found.");
            //}

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null) return NotFound();
            return Ok(product);
        }
    }
}
