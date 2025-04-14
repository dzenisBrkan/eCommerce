using AbySalto.Mid.WebApi.Models.ProductDto;
using Newtonsoft.Json;

namespace AbySalto.Mid.WebApi.Services.ProductItemService;

public interface IProductService
{
    Task<JsonResponse> GetAllProducts(int Page, int productsPerPage, string sortBy, string orderBy);

    Task<JsonResponse> SaveAllProductToDatabase();

    Task<Models.ProductDto.Product> GetProductById(int id);
   
    Task<JsonResponse> SearchProducts(string query);
}