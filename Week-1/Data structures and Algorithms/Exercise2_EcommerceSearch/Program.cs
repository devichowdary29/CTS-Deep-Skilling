using System;
using System.Linq;

namespace Exercise2_EcommerceSearch
{
    public class Product : IComparable<Product>
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }

        // Implementing IComparable to allow sorting by ProductId
        public int CompareTo(Product other)
        {
            return string.Compare(this.ProductId, other.ProductId, StringComparison.Ordinal);
        }

        public override string ToString()
        {
            return $"[{ProductId}] {ProductName} ({Category})";
        }
    }

    class Program
    {
        // Linear Search
        static Product LinearSearch(Product[] products, string targetId)
        {
            foreach (var product in products)
            {
                if (product.ProductId == targetId)
                    return product;
            }
            return null;
        }

        // Binary Search
        static Product BinarySearch(Product[] products, string targetId)
        {
            int left = 0;
            int right = products.Length - 1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                int comparison = string.Compare(products[mid].ProductId, targetId, StringComparison.Ordinal);

                if (comparison == 0)
                    return products[mid];
                
                if (comparison < 0)
                    left = mid + 1; // Target is in the right half
                else
                    right = mid - 1; // Target is in the left half
            }

            return null;
        }

        static void Main(string[] args)
        {
            Product[] products = new Product[]
            {
                new Product { ProductId = "P105", ProductName = "Tablet", Category = "Electronics" },
                new Product { ProductId = "P101", ProductName = "Laptop", Category = "Electronics" },
                new Product { ProductId = "P103", ProductName = "Desk", Category = "Furniture" },
                new Product { ProductId = "P102", ProductName = "Chair", Category = "Furniture" },
                new Product { ProductId = "P104", ProductName = "Mouse", Category = "Electronics" }
            };

            Console.WriteLine("--- Linear Search ---");
            Product foundLinear = LinearSearch(products, "P103");
            Console.WriteLine(foundLinear != null ? $"Found: {foundLinear}" : "Not Found");

            Console.WriteLine("\n--- Binary Search ---");
            // Binary search requires a sorted array
            Array.Sort(products);
            Console.WriteLine("Array sorted by ProductId.");
            Product foundBinary = BinarySearch(products, "P103");
            Console.WriteLine(foundBinary != null ? $"Found: {foundBinary}" : "Not Found");
        }
    }
}
