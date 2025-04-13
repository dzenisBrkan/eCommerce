namespace AbySalto.Mid.Domain.Entities;

public class ProductTag
{
    public int Id { get; set; }
    public string Value { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
}