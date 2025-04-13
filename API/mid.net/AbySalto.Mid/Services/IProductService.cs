using AbySalto.Mid.WebApi.Models;

namespace AbySalto.Mid.WebApi.Services;

public interface IProductService
{
    Task<JsonResponse> GetAllProducts(int Page, int productsPerPage, string sortBy, string orderBy);

    Task<ProductDetails> GetProductById(int id);
   
    Task<JsonResponse> SearchProducts(string query);
}