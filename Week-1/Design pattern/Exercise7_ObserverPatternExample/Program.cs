using System;
using System.Collections.Generic;

namespace ObserverPatternExample
{
    public interface IObserver
    {
        void Update(string stockSymbol, double price);
    }

    public interface IStock
    {
        void RegisterObserver(IObserver observer);
        void DeregisterObserver(IObserver observer);
        void NotifyObservers();
    }

    public class StockMarket : IStock
    {
        private List<IObserver> _observers = new List<IObserver>();
        private string _stockSymbol;
        private double _price;

        public void RegisterObserver(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void DeregisterObserver(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Update(_stockSymbol, _price);
            }
        }

        public void SetStockPrice(string stockSymbol, double price)
        {
            _stockSymbol = stockSymbol;
            _price = price;
            NotifyObservers();
        }
    }

    public class MobileApp : IObserver
    {
        public void Update(string stockSymbol, double price)
        {
            Console.WriteLine($"MobileApp Notified: {stockSymbol} is now ${price}");
        }
    }

    public class WebApp : IObserver
    {
        public void Update(string stockSymbol, double price)
        {
            Console.WriteLine($"WebApp Notified: {stockSymbol} is now ${price}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            StockMarket market = new StockMarket();
            
            MobileApp mobileApp = new MobileApp();
            WebApp webApp = new WebApp();

            market.RegisterObserver(mobileApp);
            market.RegisterObserver(webApp);

            Console.WriteLine("Updating MSFT price...");
            market.SetStockPrice("MSFT", 305.50);
            
            Console.WriteLine("\nUpdating AAPL price...");
            market.SetStockPrice("AAPL", 150.25);
        }
    }
}
