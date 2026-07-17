using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EFCore.BulkExtensions;

namespace RetailInventory
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("=== Retail Inventory EF Core 8.0 Demo ===");

            using var context = new AppDbContext();

            // ---------------------------------------------------------
            // Lab 4: Inserting Initial Data (Seed data provides some)
            // ---------------------------------------------------------
            Console.WriteLine("\n[Lab 4] Inserting Data...");
            if (!await context.Products.AnyAsync(p => p.Name == "Laptop"))
            {
                var electronics = await context.Categories.FirstOrDefaultAsync(c => c.Name == "Electronics") 
                                  ?? new Category { Name = "Electronics" };
                var groceries = await context.Categories.FirstOrDefaultAsync(c => c.Name == "Groceries") 
                                ?? new Category { Name = "Groceries" };

                var product1 = new Product { Name = "Laptop", Price = 75000, Category = electronics, StockQuantity = 10 };
                var product2 = new Product { Name = "Rice Bag", Price = 1200, Category = groceries, StockQuantity = 200 };
                
                await context.Products.AddRangeAsync(product1, product2);
                await context.SaveChangesAsync();
                Console.WriteLine("Inserted Laptop and Rice Bag.");
            }

            // ---------------------------------------------------------
            // Lab 5: Retrieving Data
            // ---------------------------------------------------------
            Console.WriteLine("\n[Lab 5] Retrieving Data...");
            var products = await context.Products.ToListAsync();
            foreach (var p in products)
            {
                Console.WriteLine($"{p.Name} - ₹{p.Price} (Stock: {p.StockQuantity})");
            }

            var productById = await context.Products.FindAsync(1);
            Console.WriteLine($"Found by ID 1: {productById?.Name}");

            var expensive = await context.Products.FirstOrDefaultAsync(p => p.Price > 50000);
            Console.WriteLine($"Expensive Product (>50k): {expensive?.Name}");

            // ---------------------------------------------------------
            // Lab 6: Updating and Deleting Records
            // ---------------------------------------------------------
            Console.WriteLine("\n[Lab 6] Updating and Deleting...");
            var laptopToUpdate = await context.Products.FirstOrDefaultAsync(p => p.Name == "Laptop");
            if (laptopToUpdate != null)
            {
                laptopToUpdate.Price = 70000;
                await context.SaveChangesAsync();
                Console.WriteLine("Updated Laptop price to 70000.");
            }

            var toDelete = await context.Products.FirstOrDefaultAsync(p => p.Name == "Rice Bag");
            if (toDelete != null)
            {
                context.Products.Remove(toDelete);
                await context.SaveChangesAsync();
                Console.WriteLine("Deleted Rice Bag.");
            }

            // ---------------------------------------------------------
            // Lab 7: Writing Queries with LINQ
            // ---------------------------------------------------------
            Console.WriteLine("\n[Lab 7] LINQ Queries...");
            var filtered = await context.Products
                .Where(p => p.Price > 1000)
                .OrderByDescending(p => p.Price)
                .ToListAsync();
            Console.WriteLine($"Found {filtered.Count} products over ₹1000.");

            var productDTOs = await context.Products
                .Select(p => new ProductDTO { Name = p.Name, CategoryName = p.Category.Name })
                .ToListAsync();
            foreach (var dto in productDTOs)
            {
                Console.WriteLine($"DTO: {dto.Name} in {dto.CategoryName}");
            }

            // ---------------------------------------------------------
            // Lab 10: Eager, Lazy, and Explicit Loading
            // ---------------------------------------------------------
            Console.WriteLine("\n[Lab 10] Loading Strategies...");
            // Eager
            var eagerProducts = await context.Products.Include(p => p.Category).ToListAsync();
            Console.WriteLine("Eager loading completed.");
            
            // Explicit
            var explicitProduct = await context.Products.FirstAsync();
            await context.Entry(explicitProduct).Reference(p => p.Category).LoadAsync();
            Console.WriteLine($"Explicitly loaded category for {explicitProduct.Name}: {explicitProduct.Category.Name}");

            // ---------------------------------------------------------
            // Lab 13: Query Caching and Tracking Behavior
            // ---------------------------------------------------------
            Console.WriteLine("\n[Lab 13] AsNoTracking & Compiled Queries...");
            var noTrackingProducts = await context.Products.AsNoTracking().ToListAsync();
            Console.WriteLine($"Retrieved {noTrackingProducts.Count} products without tracking.");

            // Compiled Query
            var compiledQuery = EF.CompileAsyncQuery((AppDbContext ctx, decimal minPrice) =>
                ctx.Products.Where(p => p.Price > minPrice));
                
            int count = 0;
            await foreach (var p in compiledQuery(context, 10000))
            {
                count++;
            }
            Console.WriteLine($"Compiled query found {count} products over ₹10000.");

            // ---------------------------------------------------------
            // Lab 14: Batch Processing and Bulk Operations
            // ---------------------------------------------------------
            Console.WriteLine("\n[Lab 14] Bulk Update...");
            // Using EFCore.BulkExtensions
            var allProducts = await context.Products.ToListAsync();
            foreach (var p in allProducts)
            {
                p.StockQuantity += 1;
            }
            await context.BulkUpdateAsync(allProducts);
            Console.WriteLine("Bulk updated all products (added 1 to StockQuantity).");

            // ---------------------------------------------------------
            // Lab 15: Handling Concurrency with RowVersion
            // ---------------------------------------------------------
            Console.WriteLine("\n[Lab 15] Concurrency Handling...");
            try
            {
                using var context2 = new AppDbContext();
                
                var p1 = await context.Products.FirstAsync();
                var p2 = await context2.Products.FindAsync(p1.Id);

                p1.Price += 100; // User 1
                p2.Price += 200; // User 2

                await context.SaveChangesAsync(); // User 1 saves successfully
                await context2.SaveChangesAsync(); // User 2 tries to save and throws exception
            }
            catch (DbUpdateConcurrencyException)
            {
                Console.WriteLine("SUCCESS: Concurrency conflict detected and caught safely.");
            }

            Console.WriteLine("\nAll labs executed successfully!");
        }
    }
}
