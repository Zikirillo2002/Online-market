using Lesson11.Models;
using Microsoft.EntityFrameworkCore;

namespace Lesson11.Data
{
    public class DiyorMarketDbContext : DbContext
    {
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Inventory> Inventories { get; set; }
        public virtual DbSet<InventoryItem> InventoryItems { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<SaleItem> SaleItems { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Supply> Supplies { get; set; }
        public virtual DbSet<SupplyItem> SupplyItems { get; set; }

        public DiyorMarketDbContext(DbContextOptions<DiyorMarketDbContext> options) :
            base(options)
        {
            Database.Migrate();
        }
    }
}
