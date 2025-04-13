using System.ComponentModel.DataAnnotations.Schema;

namespace AbySalto.Mid.Domain.Entities
{
    public class Bucket
    {
        public int Id { get; set; }

        [ForeignKey("UserId")]
        public int? UserId { get; set; }
        public User User { get; set; }

        public ICollection<Product> Items { get; set; } = new List<Product>();

        public decimal Total { get; set; }
        public int TotalQuantity { get; set; }
        public int TotalProducts { get; set; }
        public DateTime order_date { get; set; }
        public string status { get; set; }
        public string order_notes { get; set; }
    }
}
