using AbySalto.Mid.WebApi.Models.ProductDto;

namespace AbySalto.Mid.WebApi.Services.ProductItemService;

public interface IProductService
{
    Task<JsonResponse> GetAllProducts(int Page, int productsPerPage, string sortBy, string orderBy);

    Task<Product> GetProductById(int id);
   
    Task<JsonResponse> SearchProducts(string query);
}