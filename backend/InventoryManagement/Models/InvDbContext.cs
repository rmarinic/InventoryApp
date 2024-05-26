using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Models
{
    public class InvDbContext : DbContext
    {

        public InvDbContext(DbContextOptions<InvDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Stock> Stocks { get; set; }
    }
}
