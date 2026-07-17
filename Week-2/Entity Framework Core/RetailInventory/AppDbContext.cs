using Microsoft.EntityFrameworkCore;

namespace RetailInventory
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductDetail> ProductDetails { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Lab 10: Enable Lazy Loading Proxies
            // We use (localdb)\mssqllocaldb to ensure it works on your machine instantly!
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlite("Data Source=RetailInventory.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Lab 11: Configure One-to-One
            modelBuilder.Entity<ProductDetail>()
                .HasOne(pd => pd.Product)
                .WithOne(p => p.ProductDetail)
                .HasForeignKey<ProductDetail>(pd => pd.ProductId);
                
            // Lab 9: Seed Data during migration
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Electronics" },
                new Category { Id = 2, Name = "Groceries" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Smartphone", Price = 25000, CategoryId = 1, StockQuantity = 50, RowVersion = new byte[8] },
                new Product { Id = 2, Name = "Wheat Flour", Price = 800, CategoryId = 2, StockQuantity = 100, RowVersion = new byte[8] }
            );
        }
    }
}
