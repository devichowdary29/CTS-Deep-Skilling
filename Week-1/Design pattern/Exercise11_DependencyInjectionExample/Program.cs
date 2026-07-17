using System;

namespace DependencyInjectionExample
{
    public interface ICustomerRepository
    {
        string FindCustomerById(int id);
    }

    public class CustomerRepositoryImpl : ICustomerRepository
    {
        public string FindCustomerById(int id)
        {
            // Simulating database lookup
            return $"Customer_{id} (Name: John Doe)";
        }
    }

    public class CustomerService
    {
        private readonly ICustomerRepository _repository;

        // Constructor Injection
        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public void DisplayCustomerInfo(int id)
        {
            string customerInfo = _repository.FindCustomerById(id);
            Console.WriteLine($"Service retrieved: {customerInfo}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Setup DI manually
            ICustomerRepository repository = new CustomerRepositoryImpl();
            CustomerService service = new CustomerService(repository);

            Console.WriteLine("Fetching customer with ID 1:");
            service.DisplayCustomerInfo(1);
        }
    }
}
