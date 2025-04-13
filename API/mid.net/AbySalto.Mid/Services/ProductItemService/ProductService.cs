using AbySalto.Mid.WebApi.Models.ProductDto;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Text.Json;
namespace AbySalto.Mid.WebApi.Services.ProductItemService;

public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;
    private readonly IMemoryCache _cache;
    //private readonly ApplicationDbContext _context;

    public ProductService(HttpClient httpClient, IMemoryCache cache /*, ApplicationDbContext context*/)
    {
        _httpClient = httpClient;
        _cache = cache;
        //_context = context;
    }

    public async Task<JsonResponse> GetAllProducts(int Page, int productsPerPage, string sortBy, string orderBy)
    {
        int skip = (Page - 1) * productsPerPage;
        string url = $"https://dummyjson.com/products?skip={skip}&limit={productsPerPage}&sortBy={sortBy}&&order={orderBy}";

        // Generate a unique cache key
        string cacheKey = $"products_{Page}_{productsPerPage}_{sortBy}_{orderBy}";

        // Try to get from cache
        if (_cache.TryGetValue(cacheKey, out JsonResponse cachedResponse))
        {
            return cachedResponse;
        }

        var response = await _httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            var productResponse = JsonConvert.DeserializeObject<JsonResponse>(jsonString);

            // Store in cache for 5 minutes
            _cache.Set(cacheKey, productResponse, TimeSpan.FromMinutes(5));

            return productResponse ?? new JsonResponse();
        }

        return new JsonResponse();
    }

    public async Task<Product> GetProductById(int id)
    {
        var response = await _httpClient.GetAsync($"https://dummyjson.com/products/{id}");

        if (response.IsSuccessStatusCode)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<Product>(jsonString);
            return product;
        }

        return null;
    }

    public async Task<JsonResponse> SearchProducts(string query)
    {
        var url = $"https://dummyjson.com/products/search?q={query}";
        var response = await _httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            var productResponse = JsonConvert.DeserializeObject<JsonResponse>(jsonString);
            return productResponse ?? new JsonResponse();
        }

        return new JsonResponse();
    }
}