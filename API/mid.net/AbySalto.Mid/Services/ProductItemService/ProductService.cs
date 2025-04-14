using AbySalto.Mid.Domain.Data;
using AbySalto.Mid.Domain.Entities;
using AbySalto.Mid.WebApi.Models.ProductDto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Globalization;
using System.Text.Json;
namespace AbySalto.Mid.WebApi.Services.ProductItemService;

public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;
    private readonly IMemoryCache _cache;
    private readonly ApplicationDbContext _context;

    public ProductService(HttpClient httpClient, IMemoryCache cache, ApplicationDbContext context)
    {
        _httpClient = httpClient;
        _cache = cache;
        _context = context;
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

            foreach (var product in productResponse.Products)
            {
                product.IsFavorite = await _context.Favorites.AnyAsync(x => x.ProductId == product.Id);
            }

            // Store in cache for 5 minutes
            _cache.Set(cacheKey, productResponse, TimeSpan.FromMinutes(5));

            return productResponse ?? new JsonResponse();
        }

        return new JsonResponse();
    }

    public async Task<JsonResponse> SaveAllProductToDatabase()
    {
        string url = $"https://dummyjson.com/products?limit=194";
        var response = await _httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<JsonResponse>(jsonString);


            foreach (var item in products.Products)
            {
                // Check if product exists
                var exists = await _context.Products.AnyAsync(p => p.Title == item.Title);
                if (!exists)
                {
                    var product = new Domain.Entities.Product
                    {
                        Title = item.Title,
                        Description = item.Description,
                        Category = item.Category,
                        Price = item.Price,
                        DiscountPercentage = item.DiscountPercentage,
                        Rating = item.Rating,
                        Stock = item.Stock,
                        Brand = (string?)item.Brand,
                        Sku = item.Id.ToString(),
                        Weight = 1.0f,
                        WarrantyInformation = "Default Warranty",
                        ShippingInformation = "Ships within 3-5 days",
                        AvailabilityStatus = "In Stock",
                        ReturnPolicy = "30-day return",
                        MinimumOrderQuantity = 1,
                        Thumbnail = item.Thumbnail,
                        Tags = new List<ProductTag>(),
                        Images = item.Images?.Select(img => new ProductImage { Url = img }).ToList(),
                        Reviews = new List<Domain.Entities.Review>(),
                        Dimensions = new Domain.Entities.Dimensions { Width = 1, Height = 1, Depth = 1 },
                        Meta = new Domain.Entities.Meta
                        {
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow,
                            Barcode = Guid.NewGuid().ToString(),
                            QrCode = Guid.NewGuid().ToString()
                        }
                    };

                    _context.Products.Add(product);
                }
            }
            await _context.SaveChangesAsync();
        }

        return new JsonResponse();
    }

    public async Task<Models.ProductDto.Product> GetProductById(int id)
    {
        var response = await _httpClient.GetAsync($"https://dummyjson.com/products/{id}");

        if (response.IsSuccessStatusCode)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<Models.ProductDto.Product>(jsonString);
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