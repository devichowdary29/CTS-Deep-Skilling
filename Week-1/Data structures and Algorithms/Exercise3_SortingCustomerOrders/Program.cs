using System;

namespace Exercise3_SortingCustomerOrders
{
    public class Order
    {
        public string OrderId { get; set; }
        public string CustomerName { get; set; }
        public double TotalPrice { get; set; }

        public override string ToString()
        {
            return $"Order: {OrderId}, Customer: {CustomerName}, Total: ${TotalPrice}";
        }
    }

    class Program
    {
        static void BubbleSort(Order[] orders)
        {
            int n = orders.Length;
            for (int i = 0; i < n - 1; i++)
            {
                bool swapped = false;
                for (int j = 0; j < n - i - 1; j++)
                {
                    // Sort descending (highest value first)
                    if (orders[j].TotalPrice < orders[j + 1].TotalPrice)
                    {
                        var temp = orders[j];
                        orders[j] = orders[j + 1];
                        orders[j + 1] = temp;
                        swapped = true;
                    }
                }
                if (!swapped) break;
            }
        }

        static void QuickSort(Order[] orders, int low, int high)
        {
            if (low < high)
            {
                int pi = Partition(orders, low, high);
                QuickSort(orders, low, pi - 1);
                QuickSort(orders, pi + 1, high);
            }
        }

        static int Partition(Order[] orders, int low, int high)
        {
            double pivot = orders[high].TotalPrice;
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                // Sort descending (highest value first)
                if (orders[j].TotalPrice >= pivot)
                {
                    i++;
                    var temp = orders[i];
                    orders[i] = orders[j];
                    orders[j] = temp;
                }
            }

            var temp1 = orders[i + 1];
            orders[i + 1] = orders[high];
            orders[high] = temp1;

            return i + 1;
        }

        static void Main(string[] args)
        {
            Order[] orders1 = {
                new Order { OrderId = "O1", CustomerName = "Alice", TotalPrice = 250.50 },
                new Order { OrderId = "O2", CustomerName = "Bob", TotalPrice = 15.00 },
                new Order { OrderId = "O3", CustomerName = "Charlie", TotalPrice = 900.00 },
                new Order { OrderId = "O4", CustomerName = "David", TotalPrice = 120.75 }
            };

            // Copy array for QuickSort
            Order[] orders2 = (Order[])orders1.Clone();

            Console.WriteLine("--- Bubble Sort ---");
            BubbleSort(orders1);
            foreach (var o in orders1) Console.WriteLine(o);

            Console.WriteLine("\n--- Quick Sort ---");
            QuickSort(orders2, 0, orders2.Length - 1);
            foreach (var o in orders2) Console.WriteLine(o);
        }
    }
}
