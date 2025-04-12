using AbySalto.Mid.WebApi.Models;

namespace AbySalto.Mid.WebApi.Services;

public interface IProductService
{
    Task<JsonResponse> GetAllProducts(int page = 1, int productsPerPage = 10);

    Task<Product> GetProductById(int id);
   
    Task<JsonResponse> SearchProducts(string query);
}