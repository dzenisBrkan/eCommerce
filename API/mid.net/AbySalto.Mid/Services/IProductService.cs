using AbySalto.Mid.WebApi.Models;

namespace AbySalto.Mid.WebApi.Services;

public interface IProductService
{
    Task<Product> GetProductById(int id);
    Task<JsonResponse> GetAllProducts();
}
