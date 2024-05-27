using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.Models
{
    [Table("stock")]
    public class Stock
    {
        [Key]
        public int stockId { get; set; }
        public int productId { get; set; }
        public int quantity { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime lastModifiedDate { get; set; }
    }
}
