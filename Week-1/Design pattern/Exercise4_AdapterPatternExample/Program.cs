using System;

namespace AdapterPatternExample
{
    public interface IPaymentProcessor
    {
        void ProcessPayment(double amount);
    }

    public class LegacyStripeGateway
    {
        public void MakePayment(double amount)
        {
            Console.WriteLine($"Payment of ${amount} processed via Stripe.");
        }
    }

    public class LegacyPayPalGateway
    {
        public void SendPayment(double amount)
        {
            Console.WriteLine($"Payment of ${amount} processed via PayPal.");
        }
    }

    public class StripeAdapter : IPaymentProcessor
    {
        private LegacyStripeGateway _stripeGateway;

        public StripeAdapter(LegacyStripeGateway stripeGateway)
        {
            _stripeGateway = stripeGateway;
        }

        public void ProcessPayment(double amount)
        {
            _stripeGateway.MakePayment(amount);
        }
    }

    public class PayPalAdapter : IPaymentProcessor
    {
        private LegacyPayPalGateway _paypalGateway;

        public PayPalAdapter(LegacyPayPalGateway paypalGateway)
        {
            _paypalGateway = paypalGateway;
        }

        public void ProcessPayment(double amount)
        {
            _paypalGateway.SendPayment(amount);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            IPaymentProcessor stripeProcessor = new StripeAdapter(new LegacyStripeGateway());
            stripeProcessor.ProcessPayment(100.50);

            IPaymentProcessor paypalProcessor = new PayPalAdapter(new LegacyPayPalGateway());
            paypalProcessor.ProcessPayment(250.75);
        }
    }
}
