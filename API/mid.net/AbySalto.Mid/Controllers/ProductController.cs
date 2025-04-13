﻿using AbySalto.Mid.WebApi.Models;
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
        public async Task<ActionResult<JsonResponse>> Get([FromQuery] int page, [FromQuery] int productsPerPage, [FromQuery] string sortBy, [FromQuery] string orderBy)
        {
            if (page < 1) page = 1;
            if (productsPerPage < 1) productsPerPage = 10;
            if (sortBy == null) sortBy = "title";
            if (orderBy == null) orderBy = "asc";

            var products = await _productService.GetAllProducts(page, productsPerPage, sortBy, orderBy);
            if (products == null)
            {
                return NotFound("No products found.");
            }

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpGet("search")]
        public async Task<ActionResult<JsonResponse>> SearchProducts([FromQuery] string q)
        {
            if (string.IsNullOrWhiteSpace(q))
                return BadRequest("Query cannot be empty.");

            var products = await _productService.SearchProducts(q);
            return Ok(products);
        }
    }
}
