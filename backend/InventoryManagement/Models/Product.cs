using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.Models
{
    [Table("product")]
    public class Product
    {
        [Key]
        public int productId { get; set; }
        public string productName { get; set; }
        public string categoryName { get; set; }
        public DateTime createdDate { get; set; }
        public float price { get; set; }
        public string productDetails { get; set; }
    }
}
