using AbySalto.Mid.WebApi.Models;
using Newtonsoft.Json;
using System.Text.Json;
namespace AbySalto.Mid.WebApi.Services;

public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;
    //private readonly ApplicationDbContext _context;

    public ProductService(HttpClient httpClient /*, ApplicationDbContext context*/)
    {
        _httpClient = httpClient;
        //_context = context;
    }

    public async Task<JsonResponse> GetAllProducts(int Page, int productsPerPage)
    {
        int skip = (Page - 1) * productsPerPage;
        string url = $"https://dummyjson.com/products?skip={skip}&limit={productsPerPage}";
        var response = await _httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            var productResponse = JsonConvert.DeserializeObject<JsonResponse>(jsonString);
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
}