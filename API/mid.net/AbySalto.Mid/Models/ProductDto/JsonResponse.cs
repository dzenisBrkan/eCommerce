namespace AbySalto.Mid.WebApi.Models.ProductDto;

public class JsonResponse
{
    public List<Product> Products { get; set; }
    public int Total { get; set; }
    public int Skip { get; set; }
    public int Limit { get; set; }
}
