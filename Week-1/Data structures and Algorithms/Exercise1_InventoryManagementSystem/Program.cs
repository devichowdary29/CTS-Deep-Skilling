using System;
using System.Collections.Generic;

namespace Exercise1_InventoryManagementSystem
{
    public class Product
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public override string ToString()
        {
            return $"ID: {ProductId}, Name: {ProductName}, Quantity: {Quantity}, Price: ${Price}";
        }
    }

    public class Inventory
    {
        private Dictionary<string, Product> _products = new Dictionary<string, Product>();

        public void AddProduct(Product product)
        {
            if (!_products.ContainsKey(product.ProductId))
            {
                _products.Add(product.ProductId, product);
                Console.WriteLine($"Added: {product.ProductName}");
            }
            else
            {
                Console.WriteLine("Product with this ID already exists.");
            }
        }

        public void UpdateProduct(string productId, int newQuantity, decimal newPrice)
        {
            if (_products.TryGetValue(productId, out Product product))
            {
                product.Quantity = newQuantity;
                product.Price = newPrice;
                Console.WriteLine($"Updated: {product.ProductName}");
            }
            else
            {
                Console.WriteLine("Product not found.");
            }
        }

        public void DeleteProduct(string productId)
        {
            if (_products.Remove(productId))
            {
                Console.WriteLine($"Deleted product with ID: {productId}");
            }
            else
            {
                Console.WriteLine("Product not found.");
            }
        }

        public void DisplayInventory()
        {
            Console.WriteLine("\n--- Inventory ---");
            foreach (var product in _products.Values)
            {
                Console.WriteLine(product);
            }
            Console.WriteLine("-----------------\n");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Inventory inventory = new Inventory();
            
            Product p1 = new Product { ProductId = "P001", ProductName = "Laptop", Quantity = 10, Price = 999.99m };
            Product p2 = new Product { ProductId = "P002", ProductName = "Smartphone", Quantity = 50, Price = 499.99m };

            inventory.AddProduct(p1);
            inventory.AddProduct(p2);
            inventory.DisplayInventory();

            inventory.UpdateProduct("P001", 8, 949.99m);
            inventory.DisplayInventory();

            inventory.DeleteProduct("P002");
            inventory.DisplayInventory();
        }
    }
}
