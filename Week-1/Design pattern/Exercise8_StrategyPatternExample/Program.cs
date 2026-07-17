using System;

namespace StrategyPatternExample
{
    public interface IPaymentStrategy
    {
        void Pay(double amount);
    }

    public class CreditCardPayment : IPaymentStrategy
    {
        private string _cardNumber;
        public CreditCardPayment(string cardNumber)
        {
            _cardNumber = cardNumber;
        }

        public void Pay(double amount)
        {
            Console.WriteLine($"Paid ${amount} using Credit Card {_cardNumber}.");
        }
    }

    public class PayPalPayment : IPaymentStrategy
    {
        private string _email;
        public PayPalPayment(string email)
        {
            _email = email;
        }

        public void Pay(double amount)
        {
            Console.WriteLine($"Paid ${amount} using PayPal account {_email}.");
        }
    }

    public class PaymentContext
    {
        private IPaymentStrategy _paymentStrategy;

        public void SetPaymentStrategy(IPaymentStrategy paymentStrategy)
        {
            _paymentStrategy = paymentStrategy;
        }

        public void ExecutePayment(double amount)
        {
            if (_paymentStrategy == null)
            {
                Console.WriteLine("No payment strategy selected.");
                return;
            }
            _paymentStrategy.Pay(amount);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            PaymentContext context = new PaymentContext();

            context.SetPaymentStrategy(new CreditCardPayment("1234-5678-9876-5432"));
            context.ExecutePayment(150.00);

            context.SetPaymentStrategy(new PayPalPayment("user@example.com"));
            context.ExecutePayment(85.50);
        }
    }
}
