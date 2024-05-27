using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.Models
{
    [Table("purchase")]
    public class Purchase
    {
        [Key]
        public int purchaseId { get; set; }
        public DateTime purchaseDate { get; set; }
        public int productId { get; set; }
        public int quantity { get; set; }

        public string supplierName { get; set; }
        public float invoiceAmount { get; set; }
        public string invoiceNo { get; set; }
    }
}
