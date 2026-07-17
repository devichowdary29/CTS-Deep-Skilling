using System;

namespace Exercise4_EmployeeManagementSystem
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public double Salary { get; set; }

        public override string ToString()
        {
            return $"ID: {EmployeeId}, Name: {Name}, Position: {Position}, Salary: ${Salary}";
        }
    }

    class Program
    {
        static Employee[] employees = new Employee[10];
        static int count = 0;

        static void AddEmployee(Employee emp)
        {
            if (count < employees.Length)
            {
                employees[count] = emp;
                count++;
                Console.WriteLine($"Added: {emp.Name}");
            }
            else
            {
                Console.WriteLine("Array is full. Cannot add more employees.");
            }
        }

        static Employee SearchEmployee(int id)
        {
            for (int i = 0; i < count; i++)
            {
                if (employees[i].EmployeeId == id)
                    return employees[i];
            }
            return null;
        }

        static void TraverseEmployees()
        {
            Console.WriteLine("\n--- Employee List ---");
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine(employees[i]);
            }
            Console.WriteLine("---------------------\n");
        }

        static void DeleteEmployee(int id)
        {
            int indexToDelete = -1;
            for (int i = 0; i < count; i++)
            {
                if (employees[i].EmployeeId == id)
                {
                    indexToDelete = i;
                    break;
                }
            }

            if (indexToDelete != -1)
            {
                // Shift elements to the left
                for (int i = indexToDelete; i < count - 1; i++)
                {
                    employees[i] = employees[i + 1];
                }
                employees[count - 1] = null; // Clear last element
                count--;
                Console.WriteLine($"Deleted Employee ID {id}");
            }
            else
            {
                Console.WriteLine($"Employee ID {id} not found.");
            }
        }

        static void Main(string[] args)
        {
            AddEmployee(new Employee { EmployeeId = 1, Name = "Alice", Position = "Manager", Salary = 80000 });
            AddEmployee(new Employee { EmployeeId = 2, Name = "Bob", Position = "Developer", Salary = 60000 });
            AddEmployee(new Employee { EmployeeId = 3, Name = "Charlie", Position = "Designer", Salary = 55000 });

            TraverseEmployees();

            Console.WriteLine("Searching for Employee ID 2:");
            var emp = SearchEmployee(2);
            Console.WriteLine(emp != null ? emp.ToString() : "Not Found");

            Console.WriteLine("\nDeleting Employee ID 2...");
            DeleteEmployee(2);
            TraverseEmployees();
        }
    }
}
