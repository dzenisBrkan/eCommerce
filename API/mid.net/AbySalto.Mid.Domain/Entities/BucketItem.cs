using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AbySalto.Mid.Domain.Entities;

public class BucketItem
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("BucketId")]
    public int BucketId { get; set; }
    public Bucket Bucket { get; set; }

    [ForeignKey(nameof(Product))]
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public decimal Total { get; set; }
    public decimal DiscountPercentage { get; set; }
    public decimal DiscountedPrice { get; set; }
    public string Thumbnail { get; set; }
}