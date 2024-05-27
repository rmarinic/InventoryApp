using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.Models
{
    [Table("sale")]
    public class Sale
    {
        [Key]
        public int saleId { get; set; }
        public string invoiceNo { get; set; }
        public string customerName { get; set; }
        public string phoneNumber { get; set; }
        public DateTime saleDate { get; set; }
        public int productId { get; set; }
        public int quantity { get; set; }
        public int totalAmount { get; set; }
    }
}
